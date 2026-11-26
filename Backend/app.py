import argparse
import html
import re
import sqlite3
from contextlib import closing
from datetime import date
from http import HTTPStatus
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from pathlib import Path
from string import Template
from urllib.parse import parse_qsl, urlencode, urlsplit

ROOT = Path(__file__).resolve().parent
STATIC = ROOT / "static"
DATABASE = ROOT / "data" / "so-thu-chi.sqlite3"
TEMPLATE = Template((STATIC / "index.html").read_text(encoding="utf-8"))
FILTERS = {"all", "done", "pending"}
MUTATION_PATH = re.compile(r"/transactions(?:/([1-9][0-9]{0,17})/(edit|status|delete))?")


def connect_database():
    connection = sqlite3.connect(DATABASE)
    connection.row_factory = sqlite3.Row
    return connection


def initialize_database():
    schema = (ROOT / "data" / "schema.sql").read_text(encoding="utf-8")
    with closing(connect_database()) as connection:
        connection.executescript(schema)


def selected_filter(value):
    return value if value in FILTERS else "all"


def money(amount):
    return f"{amount:,}".replace(",", ".") + " ₫"


def option(value, expected):
    return "selected" if value == expected else ""


def validate_entry(fields):
    description = fields.get("description", "").strip()
    kind = fields.get("type", "")
    amount = fields.get("amount", "")
    entry_date = fields.get("date", "")
    status = fields.get("status", "")

    if not 1 <= len(description) <= 100:
        raise ValueError("Vui lòng nhập nội dung từ 1 đến 100 ký tự.")
    if kind not in {"income", "expense"} or status not in {"done", "pending"}:
        raise ValueError("Loại giao dịch hoặc trạng thái không hợp lệ.")
    if not re.fullmatch(r"[0-9]{1,12}", amount) or int(amount) < 1:
        raise ValueError("Vui lòng nhập số tiền nguyên từ 1 đến 999.999.999.999 đồng.")
    try:
        parsed_date = date.fromisoformat(entry_date)
        if entry_date != parsed_date.isoformat() or parsed_date.year < 1900:
            raise ValueError
    except ValueError:
        raise ValueError("Vui lòng nhập ngày hợp lệ từ năm 1900 trở đi.") from None

    return description, kind, int(amount), parsed_date.isoformat(), status


def render_transaction(entry, active_filter):
    entry_id = entry["id"]
    description = html.escape(entry["description"])
    day = entry["date"]
    display_date = "/".join(reversed(day.split("-")))
    kind = entry["type"]
    sign = "+" if kind == "income" else "−"
    is_done = entry["status"] == "done"
    status_label = "Đã hoàn tất" if is_done else "Chờ thực hiện"
    status_action = "Chuyển sang chờ" if is_done else "Đánh dấu hoàn tất"
    done_class = "done" if is_done else ""
    edit_query = urlencode({"filter": active_filter, "edit": entry_id})
    filter_field = f'<input type="hidden" name="filter" value="{active_filter}">'
    return f"""
        <li>
          <div>
            <div class="description">{description}</div>
            <time class="date" datetime="{day}">{display_date}</time>
          </div>
          <div class="amount {kind}">{sign}{money(entry["amount"])}</div>
          <form class="status-form" method="post" action="/transactions/{entry_id}/status">
            {filter_field}
            <button class="status {done_class}" type="submit" title="{status_action}"
                    aria-label="{status_action} cho {description}">{status_label} ↔</button>
          </form>
          <div class="actions">
            <a class="button-link" href="/?{html.escape(edit_query)}" aria-label="Sửa {description}">Sửa</a>
            <form method="post" action="/transactions/{entry_id}/delete" data-confirm-delete>
              {filter_field}
              <button class="remove" type="submit" aria-label="Xóa {description}">Xóa</button>
            </form>
          </div>
        </li>"""


def render_page(active_filter, entries, editing_id=None, fields=None, error=""):
    total_income = sum(
        entry["amount"] for entry in entries if entry["type"] == "income" and entry["status"] == "done"
    )
    total_expense = sum(
        entry["amount"] for entry in entries if entry["type"] == "expense" and entry["status"] == "done"
    )
    pending_count = sum(entry["status"] == "pending" for entry in entries)
    visible_entries = [
        entry for entry in entries if active_filter == "all" or entry["status"] == active_filter
    ]
    if visible_entries:
        rows = "".join(render_transaction(entry, active_filter) for entry in visible_entries)
        entry_list = f"<ul>{rows}</ul>"
    else:
        message = "Không có giao dịch ở trạng thái này." if entries else "Chưa có giao dịch. Hãy thêm khoản thu hoặc chi đầu tiên."
        entry_list = f'<p class="empty">{message}</p>'

    defaults = {"description": "", "type": "expense", "amount": "", "date": date.today().isoformat(), "status": "done"}
    fields = defaults if fields is None else fields
    editing = editing_id is not None
    return TEMPLATE.substitute(
        total_income=money(total_income),
        total_expense=money(total_expense),
        balance=money(total_income - total_expense),
        pending_count=pending_count,
        entry_list=entry_list,
        form_title="Sửa giao dịch" if editing else "Thêm giao dịch",
        form_action=f"/transactions/{editing_id}/edit" if editing else "/transactions",
        submit_label="Lưu thay đổi" if editing else "Thêm giao dịch",
        cancel_button=f'<a class="button-link" href="/?filter={active_filter}">Hủy sửa</a>' if editing else "",
        description=html.escape(str(fields.get("description", ""))),
        amount=html.escape(str(fields.get("amount", ""))),
        date=html.escape(str(fields.get("date", ""))),
        expense_selected=option(fields.get("type"), "expense"),
        income_selected=option(fields.get("type"), "income"),
        done_selected=option(fields.get("status"), "done"),
        pending_selected=option(fields.get("status"), "pending"),
        filter=active_filter,
        filter_all=option(active_filter, "all"),
        filter_done=option(active_filter, "done"),
        filter_pending=option(active_filter, "pending"),
        error=html.escape(error),
        error_hidden="hidden" if not error else "",
    )


class AppHandler(BaseHTTPRequestHandler):
    def send_content(self, status, content, content_type):
        self.send_response(status)
        self.send_header("Content-Type", content_type)
        self.send_header("Content-Length", str(len(content)))
        self.send_header("X-Content-Type-Options", "nosniff")
        self.send_header("Cache-Control", "no-store")
        self.end_headers()
        self.wfile.write(content)

    def send_page(self, active_filter, editing_id=None, fields=None, error=""):
        with closing(connect_database()) as connection:
            entries = connection.execute(
                "SELECT * FROM transactions ORDER BY date DESC, id DESC"
            ).fetchall()

        if editing_id and fields is None:
            entry = next((entry for entry in entries if entry["id"] == editing_id), None)
            if entry is None:
                self.send_error(HTTPStatus.NOT_FOUND, "Transaction not found")
                return
            fields = dict(entry)

        page = render_page(active_filter, entries, editing_id, fields, error)
        self.send_content(
            HTTPStatus.UNPROCESSABLE_ENTITY if error else HTTPStatus.OK,
            page.encode("utf-8"),
            "text/html; charset=utf-8",
        )

    def do_GET(self):
        target = urlsplit(self.path)
        assets = {
            "/static/style.css": ("style.css", "text/css"),
            "/static/app.js": ("app.js", "text/javascript"),
        }
        if target.path in assets:
            filename, mime_type = assets[target.path]
            self.send_content(HTTPStatus.OK, (STATIC / filename).read_bytes(), f"{mime_type}; charset=utf-8")
            return

        if target.path != "/":
            self.send_error(HTTPStatus.NOT_FOUND)
            return

        query = dict(parse_qsl(target.query))
        edit = query.get("edit", "")
        editing_id = int(edit) if re.fullmatch(r"[1-9][0-9]{0,17}", edit) else None
        self.send_page(selected_filter(query.get("filter")), editing_id)

    def do_POST(self):
        target = MUTATION_PATH.fullmatch(urlsplit(self.path).path)
        if not target:
            self.send_error(HTTPStatus.NOT_FOUND)
            return

        origin = self.headers.get("Origin")
        allowed_origins = {
            f"http://127.0.0.1:{self.server.server_port}",
            f"http://localhost:{self.server.server_port}",
        }
        if origin and origin not in allowed_origins:
            self.send_error(HTTPStatus.FORBIDDEN)
            return

        size = self.headers.get("Content-Length", "")
        if not re.fullmatch(r"[0-9]{1,4}", size):
            self.send_error(HTTPStatus.BAD_REQUEST)
            return

        try:
            fields = dict(parse_qsl(self.rfile.read(int(size)).decode("utf-8"), keep_blank_values=True))
        except UnicodeDecodeError:
            self.send_error(HTTPStatus.BAD_REQUEST)
            return

        active_filter = selected_filter(fields.get("filter"))
        raw_id, action = target.groups()
        entry_id = int(raw_id) if raw_id else None
        if action in {None, "edit"}:
            try:
                values = validate_entry(fields)
            except ValueError as error:
                self.send_page(active_filter, entry_id, fields, str(error))
                return

        with closing(connect_database()) as connection, connection:
            if action == "edit":
                result = connection.execute(
                    "UPDATE transactions SET description=?, type=?, amount=?, date=?, status=? WHERE id=?",
                    (*values, entry_id),
                )
            elif action == "status":
                result = connection.execute(
                    """UPDATE transactions SET status =
                       CASE status WHEN 'done' THEN 'pending' ELSE 'done' END WHERE id=?""",
                    (entry_id,),
                )
            elif action == "delete":
                result = connection.execute("DELETE FROM transactions WHERE id=?", (entry_id,))
            else:
                result = connection.execute(
                    "INSERT INTO transactions (description, type, amount, date, status) VALUES (?, ?, ?, ?, ?)",
                    values,
                )

        if result.rowcount == 0:
            self.send_error(HTTPStatus.NOT_FOUND, "Transaction not found")
            return

        self.send_response(HTTPStatus.SEE_OTHER)
        self.send_header("Location", f"/?filter={active_filter}")
        self.send_header("Content-Length", "0")
        self.end_headers()


def main():
    parser = argparse.ArgumentParser(description="Chạy web Sổ thu chi.")
    parser.add_argument("--port", default=8000, type=int, help="Cổng máy chủ, mặc định 8000.")
    arguments = parser.parse_args()
    initialize_database()

    with ThreadingHTTPServer(("127.0.0.1", arguments.port), AppHandler) as server:
        print(f"Mở http://127.0.0.1:{server.server_port} trong trình duyệt. Nhấn Ctrl+C để dừng.", flush=True)
        try:
            server.serve_forever()
        except KeyboardInterrupt:
            pass


if __name__ == "__main__":
    main()

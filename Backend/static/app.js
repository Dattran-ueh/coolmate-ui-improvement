for (const form of document.querySelectorAll('form[data-confirm-delete]')) {
  form.addEventListener('submit', (event) => {
    if (!window.confirm('Bạn có chắc muốn xóa giao dịch này?')) {
      event.preventDefault();
    }
  });
}

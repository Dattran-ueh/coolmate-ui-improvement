CREATE TABLE IF NOT EXISTS transactions (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    description TEXT NOT NULL CHECK (length(trim(description)) BETWEEN 1 AND 100),
    type TEXT NOT NULL CHECK (type IN ('income', 'expense')),
    amount INTEGER NOT NULL CHECK (
        typeof(amount) = 'integer' AND amount BETWEEN 1 AND 999999999999
    ),
    date TEXT NOT NULL CHECK (
        length(date) = 10 AND date BETWEEN '1900-01-01' AND '9999-12-31'
    ),
    status TEXT NOT NULL CHECK (status IN ('pending', 'done'))
);

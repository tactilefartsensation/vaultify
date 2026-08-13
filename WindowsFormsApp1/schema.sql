DROP DATABASE IF EXISTS vaultify;
CREATE DATABASE vaultify;
USE vaultify;


-- =========================================
-- EMPLOYEES
-- =========================================

CREATE TABLE employees (
    employee_id INT AUTO_INCREMENT PRIMARY KEY,
    employee_number VARCHAR(50) NOT NULL UNIQUE,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    username VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    role ENUM('EMPLOYEE', 'ADMIN') NOT NULL DEFAULT 'EMPLOYEE',
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP
);

-- =========================================
-- FINGERPRINT CREDENTIALS
-- =========================================

CREATE TABLE fingerprint_credentials (
    fingerprint_id INT AUTO_INCREMENT PRIMARY KEY,
    employee_id INT NOT NULL,
    template_data BLOB NOT NULL,
    finger_index VARCHAR(30),

    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    FOREIGN KEY (employee_id)
        REFERENCES employees(employee_id)
);

-- =========================================
-- TAXPAYERS
-- =========================================

CREATE TABLE taxpayers (
    taxpayer_id INT AUTO_INCREMENT PRIMARY KEY,
    taxpayer_number VARCHAR(50) NOT NULL UNIQUE,

    taxpayer_type ENUM('INDIVIDUAL', 'BUSINESS') NOT NULL,

    first_name VARCHAR(100),
    last_name VARCHAR(100),
    business_name VARCHAR(200),

    address VARCHAR(255),
    contact_number VARCHAR(30),
    email VARCHAR(150),

    status ENUM('ACTIVE', 'INACTIVE') NOT NULL DEFAULT 'ACTIVE',

    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP
);


-- =========================================
-- TAX RECORDS
-- =========================================

CREATE TABLE tax_records (
    tax_record_id INT AUTO_INCREMENT PRIMARY KEY,
    taxpayer_id INT NOT NULL,
    tax_type VARCHAR(100) NOT NULL,
    tax_period YEAR NOT NULL,
    amount_due DECIMAL(12,2) NOT NULL,
    due_date DATE,

    status ENUM('UNPAID', 'PARTIAL', 'PAID', 'OVERDUE')
        NOT NULL DEFAULT 'UNPAID',

    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    FOREIGN KEY (taxpayer_id)
        REFERENCES taxpayers(taxpayer_id)
);

-- =========================================
-- PAYMENTS
-- =========================================

CREATE TABLE payments (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,

    tax_record_id INT NOT NULL,
    processed_by INT NOT NULL,

    receipt_number VARCHAR(50) NOT NULL UNIQUE,
    amount_paid DECIMAL(12,2) NOT NULL,
    payment_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    payment_method ENUM('CASH', 'CHECK', 'OTHER') NOT NULL DEFAULT 'CASH',

    remarks TEXT,

    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (tax_record_id)
        REFERENCES tax_records(tax_record_id),

    FOREIGN KEY (processed_by)
        REFERENCES employees(employee_id)
);

-- =========================================
-- AUDIT LOGS
-- =========================================

CREATE TABLE audit_logs (
    log_id BIGINT AUTO_INCREMENT PRIMARY KEY,

    employee_id INT,
    action VARCHAR(50) NOT NULL,
    table_name VARCHAR(100),
    record_id INT,

    details TEXT,

    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (employee_id)
        REFERENCES employees(employee_id)
);
DROP DATABASE IF EXISTS vaultify;
CREATE DATABASE vaultify;
USE vaultify;


-- =========================================
-- EMPLOYEES & ROLES
-- Roles:
--   SYSADMIN   : Full system access, account management, role assignment, tax brackets
--   SUPERVISOR : Manages collector assignments to tax brackets
--   COLLECTOR  : Processes collection documents assigned by Admin/Supervisor
-- =========================================

CREATE TABLE employees (
    employee_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(150) NOT NULL,
    official_title VARCHAR(100) NULL,
    username VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    fingerprint_template BLOB NULL,
    role ENUM('SYSADMIN', 'SUPERVISOR', 'COLLECTOR') NOT NULL DEFAULT 'COLLECTOR',
    status ENUM('ACTIVE', 'INACTIVE') NOT NULL DEFAULT 'ACTIVE',
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP
);

-- =========================================
-- TAX BRACKETS / DOCUMENT CATEGORIES
-- Represents the collection windows/documents (General Collections, RPT Form 5-A, etc.)
-- =========================================

CREATE TABLE tax_brackets (
    bracket_id INT AUTO_INCREMENT PRIMARY KEY,
    code VARCHAR(50) NOT NULL UNIQUE,
    name VARCHAR(150) NOT NULL,
    description TEXT,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO tax_brackets (code, name, description) VALUES
('GEN_COLLECTION', 'General Collection & Miscellaneous Fees', 'Mayors Permit, Sanitation, Market Stall, Building Permit, Tricycle, Traffic Violations, etc.'),
('RPT_FORM_5A', 'Real Property Tax (Form 5-A)', 'Basic Real Property Tax & Special Education Fund (SEF) Collections / Amilyar'),
('BUSINESS_TAX', 'Business Taxes & Licenses', 'Municipal business tax assessments, renewals, and regulatory penalties');

-- =========================================
-- COLLECTOR ASSIGNMENTS
-- Managed by SysAdmins and Supervisors to assign Collectors to Tax Brackets
-- =========================================

CREATE TABLE collector_assignments (
    assignment_id INT AUTO_INCREMENT PRIMARY KEY,
    employee_id INT NOT NULL,
    bracket_id INT NOT NULL,
    assigned_by INT NOT NULL,
    assigned_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id) ON DELETE CASCADE,
    FOREIGN KEY (bracket_id) REFERENCES tax_brackets(bracket_id) ON DELETE CASCADE,
    FOREIGN KEY (assigned_by) REFERENCES employees(employee_id),
    UNIQUE (employee_id, bracket_id)
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
    bracket_id INT NULL,
    tax_type VARCHAR(100) NOT NULL,
    tax_period VARCHAR(50) NOT NULL,
    amount_due DECIMAL(12,2) NOT NULL,
    due_date DATE,
    status ENUM('UNPAID', 'PARTIAL', 'PAID', 'OVERDUE') NOT NULL DEFAULT 'UNPAID',
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (taxpayer_id) REFERENCES taxpayers(taxpayer_id),
    FOREIGN KEY (bracket_id) REFERENCES tax_brackets(bracket_id)
);

-- =========================================
-- PAYMENTS / OFFICIAL RECEIPTS
-- =========================================

CREATE TABLE payments (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,
    receipt_number VARCHAR(50) NOT NULL UNIQUE,
    tax_record_id INT NULL,
    bracket_id INT NULL,
    processed_by INT NOT NULL,
    amount_paid DECIMAL(12,2) NOT NULL,
    payment_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    payment_method ENUM('CASH', 'CHECK', 'OTHER') NOT NULL DEFAULT 'CASH',
    remarks TEXT,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (tax_record_id) REFERENCES tax_records(tax_record_id),
    FOREIGN KEY (bracket_id) REFERENCES tax_brackets(bracket_id),
    FOREIGN KEY (processed_by) REFERENCES employees(employee_id)
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
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id)
);
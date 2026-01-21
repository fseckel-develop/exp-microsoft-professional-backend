
Develop and articulate the architecture of **Role-Based Access Control (RBAC)** for real-world applications by:

- Defining roles based on system requirements  
- Assigning permissions using the principle of least privilege  
- Ensuring secure and appropriate access control  

This practice strengthens the ability to analyse application scenarios and design RBAC models tailored to real systems.



---
---
## Example 1: Healthcare Management System

### Scenario

A healthcare system must securely manage access for:

- **System Admin**
- **Doctor**
- **Nurse**
- **Patient**

---
### Step 1: Identify Roles and Responsibilities

**System Admin**:
Responsible for managing the entire system, including users, roles, and configurations.

**Doctor**: 
Provides patient care by viewing records, reviewing test results, and prescribing medication for assigned patients only.

**Nurse**:
Supports doctors by recording and updating patient vitals. Does not access prescriptions or test results.

**Patient**:
Views their own medical records and test results only.

---
### Step 2: Assign Permissions

**System Admin**:
- Full access to system resources
- Manage users and roles
- Access all patient records  

*Why?* Admins must maintain system integrity and security.

**Doctor**:
- View and update assigned patient records
- View test results for assigned patients
- Prescribe medications  

*Why?* These permissions are essential for delivering medical care while limiting access to only relevant patients.

**Nurse**:
- View and update patient vitals  

*Why?* Nurses assist with basic medical data and do not require access to sensitive treatment details.

**Patient**:
- View personal medical records and test results  

*Why?* Patients need transparency into their own health data without accessing others’ information.

---
### RBAC Summary Table – Healthcare System

| Role         | Permissions                                                                   |
| ------------ | ----------------------------------------------------------------------------- |
| System Admin | Full system access, user management, all patient records                      |
| Doctor       | View/update assigned patient records, view test results, prescribe medication |
| Nurse        | View/update patient vitals                                                    |
| Patient      | View personal medical records and test results                                |

---
### Design Explanation

- **Role Definition**: Roles reflect real healthcare responsibilities.
- **Permission Assignment**: 
  Permissions align with job functions and minimise unnecessary access.
- **Data Protection**: 
  Access is restricted to only what each role needs, reducing exposure of sensitive data.



---
---
## Example 2: E-Commerce Platform

### Scenario

An e-commerce platform must manage access for:

- **Super Admin**
- **Warehouse Staff**
- **Customer Service Agent**
- **Customer**

---
### Step 1: Identify Roles and Responsibilities

**Super Admin**:
Oversees users, inventory, and orders.

**Warehouse Staff**:
Manages inventory and shipping logistics.

**Customer Service Agent**:
Handles customer orders and communication.

**Customer**:
Browses products, places orders, and views personal order history.

---
### Step 2: Assign Permissions

**Super Admin**:
- Full access to users, inventory, and orders  

*Why?* Oversees platform operations and maintenance.

**Warehouse Staff**:
- Update inventory
- Manage shipping  

*Why?* Handles logistics without accessing customer data.

**Customer Service Agent**:
- View and manage orders
- Access customer communications  

*Why?* Resolves customer issues while protecting backend systems.

**Customer**:
- Browse products
- Place orders
- View personal order history  

*Why?* Customers interact only with their own data.

---
### RBAC Summary Table – E-Commerce Platform

| Role                   | Permissions                                               |
|------------------------|-----------------------------------------------------------|
| Super Admin            | Full access to users, inventory, and orders               |
| Warehouse Staff        | Update inventory, manage shipping                          |
| Customer Service Agent | View/manage orders, access customer communications         |
| Customer               | Browse products, place orders, view personal order history |

---
### Design Explanation

- **Role Definition**: Reflects core e-commerce operations.
- **Permission Assignment**: Supports efficiency while protecting sensitive data.
- **Security Benefit**: Minimises unauthorised access and reduces breach risks.



---
---
## Example 3: Learning Management System (LMS)

### Scenario

An LMS must manage access for:

- **Admin**
- **Instructor**
- **Student**
- **Guest**

---
### RBAC Summary Table – LMS

| Role       | Permissions                                                  |
|------------|--------------------------------------------------------------|
| Admin      | Manage users, manage courses, configure system settings      |
| Instructor | Manage own courses, grade assignments, view enrolled students|
| Student    | View enrolled courses, view assignments, view grades         |
| Guest      | View general course information only                          |

---
### Design Explanation

- **Roles** reflect the LMS hierarchy.
- **Permissions** ensure instructors manage only their courses.
- **Security** prevents guests and students from accessing private data.



---
---
## Example 4: Retail Bank System

### Scenario

A retail bank must manage access for:

- **Admin**
- **Teller**
- **Auditor**
- **Customer**

---
### RBAC Summary Table – Retail Bank

| Role     | Permissions                                                 |
| -------- | ----------------------------------------------------------- |
| Admin    | Manage accounts, manage transactions, view customer data    |
| Teller   | Process transactions, view account balances (limited scope) |
| Auditor  | Review system logs and transactions (no customer details)   |
| Customer | View own account details, view transaction history          |

---
### Design Explanation

- **Role Definition** aligns with banking operations.
- **Permission Assignment** enforces separation of duties.
- **Data Protection** ensures customer privacy and regulatory compliance.
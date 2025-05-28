###Open Banking API Development

###Type of work 
Backend Software Development with ASP.NET Core using Onion Architecture, CQRS, and MediatR

###Description
On the backend, I developed the application using ASP.NET Core and followed the Onion
Architecture to structure the project in a clean and layered manner. Initially, I implemented basic
database operations, but later refactored the application to adopt more advanced patterns and
practices.
I used MySQL as the database, and managed schema changes using EF Core Migrations to keep the
database in sync with the codebase. I implemented JWT-based authentication and securely
connected it with the database to manage user sessions and authorization.
To enhance code maintainability and separation of concerns, I integrated the CQRS pattern with
MediatR, allowing for clear distinction between queries and commands. I also added CORS policies
and custom HTTP error handling middleware to improve cross-origin communication and API
reliability.
Additionally, I implemented a Custom HttpClient Service to simplify and centralize HTTP
operations, and used FluentValidation together with a ValidationFilter to ensure proper request
validation at the API level. These practices helped create a scalable, secure, and maintainable API
infrastructure aligned with modern .NET development standards.
I also implemented OAuth 2.0 with Google Login as part of the authentication infrastructure.
Although the feature was fully developed and tested, I decided not to include it in the final version of
the banking application, as it was not considered necessary for the project's scope.
The API endpoints were documented using Swagger, and I also performed extensive testing through
Postman to ensure that all endpoints functioned as expected.
As part of the currency module, I developed a service that fetches real-time exchange rate data from
the Central Bank of the Republic of Turkey (CBRT) using a public XML-based API, and integrated
it into the system to display current exchange rates to users.

### 🟢 Welcome Page
<img width="800" alt="Homepage_wol" src="https://github.com/user-attachments/assets/eef0802c-09fe-4da4-ba83-9b17454902d3" />
<img width="800" alt="Homepage_wol2" src="https://github.com/user-attachments/assets/f89c67af-c3c3-43ad-8f94-a67192e6263b" />

### 🔐 Login Page
<img width="800" alt="Login_p" src="https://github.com/user-attachments/assets/e8efcb15-331a-4f53-83fd-20c6069adb9c" />

### ✅ After Login - Success View
<img width="800" alt="After_login" src="https://github.com/user-attachments/assets/b761412a-0488-4e8a-9f0c-a4d1351054c5" />

### 📝 Register 
<img width="800" alt="Register_p" src="https://github.com/user-attachments/assets/2db4f52c-b013-4d98-bbd0-e619c816e099" />

### 📝 Register - Form
<img width="800" alt="Register_p2" src="https://github.com/user-attachments/assets/3faf62c4-5cc6-4847-9f37-d0a3613b9619" />

### 📝 Register - Success
<img width="800" alt="Register_p4" src="https://github.com/user-attachments/assets/8c999046-952e-41dc-8e59-c7981180fe15" />

### ❌ Register - Error (Password Rules)
<img width="800" alt="Register_p3" src="https://github.com/user-attachments/assets/c5de87d6-22f5-40a6-83c0-ae195d925fdb" />

### 🔄 Transfer Page - Empty State
<img width="800" alt="Transaction_p_wol" src="https://github.com/user-attachments/assets/5781084b-8a2c-4ec6-ba96-4eefb1149c87" />

### 🔄 Transfer Page - After Login 
<img width="800" alt="Transaction_logged" src="https://github.com/user-attachments/assets/1a7f92ff-5778-4249-8fcf-b0f07b034d5d" />

### 🔄 Transfer Page - After Account Selection
<img width="800" alt="Transaction_logged2" src="https://github.com/user-attachments/assets/3e149a43-2861-44eb-9c74-186a8ec08699" />

### 🧾 Admin Panel - Transaction History
<img width="800" alt="AdminPanel_TransactionHist" src="https://github.com/user-attachments/assets/f4248f0c-3210-40e8-85ab-bb5930aa04c7" />

### 🏦 Admin Panel - Banks
<img width="800" alt="AdminPanel_Banks" src="https://github.com/user-attachments/assets/74b5994d-3bc0-4aec-8897-ecef96a6285e" />

### 📂 Admin Panel - Accounts
<img width="800" alt="AdminPanel_Accounts" src="https://github.com/user-attachments/assets/e8ebb358-8d09-4067-a111-f45ca4ba40d3" />

### Load Page
<img width="800" alt="Load_p" src="https://github.com/user-attachments/assets/e238a8b6-f928-46cf-9d6f-276e61f44633" />





# Home Assignment - API Implementation


## About This Project
This project is a home assignment, implemented according to the [design](01-01-Registration.pdf) flow provided in the assignment.

- This project includes all the necessary APIs as per the design.
- No third-party integrations have been included.
- No authentication or authorization is implemented.
- By default, the project uses InMemory Database for storage, but it is configured to support SQL Server as well.


## API Testing
For testing the API endpoints, `.http` files are available in the **[`Http/`](API/Http/)** folder. These files contain ready-to-use requests for each controller.

**[User.http](API/Http/User.http)**  
Handles user registration and retrieval.
- **GET /api/user** → _(For testing purposes)_ Retrieve all users.
- **POST /api/user** → Register a new user.

**[Device.http](API/Http/Device.http)**  
Manages device registration, verification, and biometrics setup.
- **GET /api/device** → _(For testing purposes)_ Retrieve all registered devices.
- **GET /api/device/pending** → _(For testing purposes)_ Get all pending devices.
- **POST /api/device/pending** → Add a pending device.
- **POST /api/device/pending/verify-mobile** → Verify device using mobile OTP.
- **POST /api/device/pending/verify-email** → Verify device using email OTP.
- **POST /api/device/pending/accept-privacy-policy** → Accept privacy policy before finalizing registration.
- **POST /api/device** → Finalize device registration.
- **POST /api/device/enable-biometrics** → Enable biometric authentication for the device.

**[Otp.http](API/Http/Otp.http)**  
Manages OTP sending and retrieval.
- **GET /api/otp** → _(For testing purposes)_ Retrieve all OTP entries.
- **POST /api/otp/mobile/send** → _(No third-party integration, OTP is printed on console)_ Send OTP to mobile.
- **POST /api/otp/email/send** → _(No third-party integration, OTP is printed on console)_ Send OTP to email.

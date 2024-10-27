# Authenticated Web API and Windows Service Client
## Overview
This solution demonstrates a .NET Core Web API with JWT authentication and a Windows Service client that accesses a protected endpoint. The API includes authentication and a protected product endpoint. The Windows Service client can authenticate and retrieve product info if authenticated.
## Solution Structure
1. **SecureApiBackend** - A Web API with JWT authentication.
2. **WindowsServiceClient** - Console application simulating a Windows Service client.
3. **SecureApiBackend.Tests** - NUnit test project for the Web API.

## Setup
1. Clone the repository.
2. Update the `appsettings.json` in `AuthApi` with JWT settings:
   ```json
   "Jwt": {
     "Key": "YourVeryLongSecureSuperSecretKey12345",
     "Issuer": "YourIssuer",
     "Audience": "YourAudience"
   }
      
### Explanation
- **SecureApiBackend**: The Web API project with JWT authentication and a protected endpoint.
- **WindowsServiceClient**: The client that authenticates, retrieves a token, and calls the protected API.
- **SecureApiBackend.Tests**: Tests for validating the authentication and protected endpoint access in the API.

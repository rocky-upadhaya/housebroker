# House Broker Application - MVP Documentation

## Overview
This document provides an overview of the **House Broker Application** designed and implemented following **Clean Architecture** principles. The application is built using **.NET**, **MSSQL**, and includes the necessary features for brokers and customers to interact with property listings.

The backend is exposed as a **Web API** where users (brokers and customers) can search for properties, and brokers can manage property listings (create, update, delete). The application uses **.NET Identity** for user authentication, allowing us to differentiate between **House Seekers (Customers)** and **Brokers**.

---

## Project Structure
The project is split into several layers, structured according to Clean Architecture principles:

- **Domain**: Contains entities, enums, and core business logic.
- **Application**: Contains application services, DTOs, and use cases for business logic.
- **Infrastructure**: Handles data persistence and external integrations (e.g., MSSQL Database, third-party APIs).
- **HouseBroker (Web API)**: The API layer that exposes endpoints for interaction.

## Features
### 1. **User Authentication**
- **User Authentication** is handled via **.NET Identity** or similar authentication mechanisms.
- Roles are differentiated between **House Seekers (Customers)** and **Brokers** during the authentication process.
- The system ensures that each user can only access resources appropriate to their role.

### 2. **Listing Management (CRUD Operations)**
- The API exposes CRUD operations to manage property listings:
  - **Create** a new property listing (only accessible by brokers).
  - **Read** existing listings (accessible by brokers and customers).
  - **Update** a property listing (only accessible by brokers).
  - **Delete** a property listing (only accessible by brokers).
- Essential details like **property type**, **location**, **price**, and **features** are captured.

### 3. **Search Functionality**
- The **Search** feature is available to both **brokers** and **customers**.
- Users can search for properties based on:
  - **Location**
  - **Price range**
  - **Property type**
- Users can apply **filters** to narrow down search results.

### 4. **Third-party API Exposure**
- The backend is designed to expose the API for third-party services to access property listings, provided they authenticate correctly.

---

## Authentication & Authorization

The application uses **JWT tokens** for authentication. Users must authenticate using valid credentials, which are verified against the database.

### Authentication Process:
1. **Login**: A user logs in with their username and password to receive a JWT token.
2. **Token Validation**: The JWT token is used for further API calls to authenticate the user.

### Role-based Authorization:
- **Broker Role**: Only brokers can manage properties (CRUD operations except search).
- **Customer Role**: Customers can search properties, but cannot create, update, or delete listings.

**Conclusion**
The House Broker Application leverages Clean Architecture principles to ensure maintainability, scalability, and separation of concerns. By adhering to these principles, the application remains modular, allowing easy testing, updating, and expansion of functionality.

The key features, such as role-based authorization, property management (CRUD), and search functionality ensure that brokers and customers have the tools they need to interact with property listings in a secure and efficient manner.


### Get Started
- Clone the code
-  `dotnet ef database update --project infrastructure --startup-project housebroker`
Run this command to in root folder.
- Build and Run
- There are three .http files to execute the api's
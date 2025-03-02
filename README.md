# 🌌 Galactic Tour Agency API

Welcome to the **Galactic Tour Agency API**! 🚀 This ASP.NET Core Web API allows users to explore and book interstellar tours, purchase galactic products, and search for cosmic experiences. 🌍✨

## 📜 Features
- 🌠 **Galactic Tours Management:** Book, update, and cancel space tours.
- 🛒 **Galactic Products:** Manage products available in different planetary markets.
- 🔍 **Advanced Search:** Find tours by planet, price range, and departure date.
- 🌍 **Custom Model Binding:** Galactic coordinates handling.
- 🛡 **Validation & Security:** Ensure data integrity with attribute-based validation.
- 📜 **Swagger Documentation:** API endpoints are documented for easy testing.

## 🏗 Technologies Used
- ⚙ **ASP.NET Core** - Backend framework
- 📡 **RESTful API** - Service communication
- 🗂 **Model Binding & Validation** - Custom attribute-based validation
- 📝 **Swagger UI** - API documentation
- 🔗 **Dependency Injection** - Service management

## 🚀 Getting Started
### Prerequisites
- .NET SDK 6.0+
- Visual Studio / VS Code
- Postman (optional for API testing)

### 🔧 Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/EfsanNart/GalacticTourAgency.git
   cd GalacticTourAgency
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Build the application:
   ```sh
   dotnet build
   ```
4. Run the API:
   ```sh
   dotnet run
   ```
5. Access Swagger UI at:
   ```
   https://localhost:5001/swagger
   ```

## 🔗 API Endpoints
### 🪐 Galactic Tours
- `GET /api/GalacticTours` → Retrieve all tours.
- `GET /api/GalacticTours/{id}` → Get a tour by ID.
- `GET /api/GalacticTours/planet/{planetName}` → Get tours by planet name.
- `POST /api/GalacticTours` → Create a new tour.
- `PUT /api/GalacticTours/update/{id}/{newPlanetName}` → Update tour location.
- `DELETE /api/GalacticTours/{id}` → Cancel a tour.

### 🛍 Galactic Products
- `GET /api/GalacticProducts/{id}` → Retrieve product by ID.
- `POST /api/GalacticProducts` → Add a new galactic product.
- `GET /api/GalacticProducts/products-at-location/{location}` → Get products at a specific location.

## 📌 Custom Validations & Model Bindings
- ✅ **Galactic Element Validation**: Ensures valid element composition for galactic products.
- 📌 **Custom Model Binding for Coordinates**: Handles galactic coordinates efficiently.

## 🛠 Contribution
We welcome contributions! 🚀 Feel free to fork this repository, make changes, and submit pull requests.

## 📜 License
This project is licensed under the MIT License.

🌟 **Happy Intergalactic Travels!** 🚀


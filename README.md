# Procure Risk Analyzer - Cross-Platform Application

A cross-platform thin client application built with .NET MAUI, implementing MVVM pattern, Identity Server authentication, and database integration with charts.

## Features

- ✅ Cross-platform support (Windows, Linux, macOS)
- ✅ MVVM pattern implementation
- ✅ Identity Server authentication
- ✅ Animated loading screen during API requests
- ✅ Three linked database tables (Suppliers, Tenders, Categories)
- ✅ Chart/graph visualization based on database data
- ✅ About window
- ✅ Full CRUD operations for all entities

## Technology Stack

- **Frontend**: .NET MAUI (Multi-platform App UI)
- **Backend**: ASP.NET Core Web API
- **Database**: SQLite (can be configured for SQL Server, PostgreSQL)
- **Authentication**: Identity Server (with fallback for demo)
- **Architecture**: MVVM (Model-View-ViewModel)
- **Charts**: Microcharts.Maui

## Project Structure

```
ProcureRiskAnalyzer/
├── ProcureRiskAnalyzer.Client/     # .NET MAUI Client Application
│   ├── Models/                     # Data models
│   ├── ViewModels/                 # MVVM ViewModels
│   ├── Views/                      # XAML pages
│   ├── Services/                   # API and authentication services
│   └── Converters/                 # Value converters
├── ProcureRiskAnalyzer.Web/        # Backend API
│   ├── Controllers/                # API controllers
│   ├── Models/                     # Database models
│   └── Data/                       # Entity Framework context
└── ProcureRiskAnalyzer/            # Core library
```

## Database Schema

The application uses three linked tables:

1. **Suppliers** - Company information
   - Id, Name, Country
   - One-to-Many relationship with Tenders

2. **Tenders** - Procurement tenders
   - Id, TenderCode, Buyer, Date, ExpectedValue
   - Foreign keys to Suppliers and Categories

3. **Categories** - Tender categories
   - Id, Name, Description
   - One-to-Many relationship with Tenders

## Getting Started

### Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or Visual Studio Code with C# extension
- For mobile platforms: Platform-specific SDKs (Android SDK, Xcode for iOS/macOS)

### Running the Application

1. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

2. **Update database (Backend):**
   ```bash
   cd ProcureRiskAnalyzer.Web
   dotnet ef database update
   ```

3. **Run the backend API:**
   ```bash
   cd ProcureRiskAnalyzer.Web
   dotnet run
   ```
   The API will be available at `https://localhost:7000` (or the port configured in launchSettings.json)

4. **Run the MAUI client:**
   ```bash
   cd ProcureRiskAnalyzer.Client
   dotnet build
   ```
   
   Then run on your platform:
   - **Windows**: `dotnet run --framework net9.0-windows10.0.19041.0`
   - **macOS**: `dotnet run --framework net9.0-maccatalyst`
   - **Linux**: Requires additional setup (see below)

### Running on Different Platforms

#### Windows
```bash
cd ProcureRiskAnalyzer.Client
dotnet run --framework net9.0-windows10.0.19041.0
```

#### macOS
```bash
cd ProcureRiskAnalyzer.Client
dotnet run --framework net9.0-maccatalyst
```

#### Linux
For Linux, you may need to use Avalonia UI or configure the project for Linux. The current setup supports Windows and macOS out of the box.

## Configuration

### Backend API Configuration

Update `ProcureRiskAnalyzer.Web/appsettings.json`:

```json
{
  "DatabaseProvider": "Sqlite",
  "ConnectionStrings": {
    "Sqlite": "Data Source=procure.db"
  }
}
```

### Client API Configuration

Update API base URL in `ProcureRiskAnalyzer.Client/Services/ApiService.cs`:
```csharp
_apiBaseUrl = "https://localhost:7000"; // Change to your API URL
```

### Identity Server Configuration

The application includes Identity Server integration. For demo purposes, it falls back to a simple authentication if Identity Server is not available.

## Features Overview

### 1. Login Screen
- Username/password authentication
- Animated loading indicator during login
- Error message display

### 2. Main Dashboard
- Navigation to all modules
- Logout functionality

### 3. Suppliers Management
- View all suppliers
- Create new supplier
- Edit existing supplier
- Delete supplier

### 4. Tenders Management
- View all tenders with linked suppliers and categories
- Create new tender (with supplier and category selection)
- Edit existing tender
- Delete tender

### 5. Categories Management
- View all categories
- Create new category
- Edit existing category
- Delete category

### 6. Statistics Page
- Bar chart showing tender values by category
- Data visualization using Microcharts

### 7. About Page
- Application information
- Version details
- Technology stack

## MVVM Pattern Implementation

The application follows MVVM pattern:

- **Models**: Data entities (Supplier, Tender, Category)
- **Views**: XAML pages (LoginPage, MainPage, etc.)
- **ViewModels**: Business logic and data binding (using CommunityToolkit.Mvvm)
- **Services**: API communication and authentication

## Identity Server Integration

The application is configured to work with Identity Server for authentication. The `AuthService` handles:
- Token acquisition
- Token refresh
- Authentication state management

For development/demo, the service includes fallback authentication.

## Building for Production

1. **Backend:**
   ```bash
   cd ProcureRiskAnalyzer.Web
   dotnet publish -c Release
   ```

2. **Client:**
   ```bash
   cd ProcureRiskAnalyzer.Client
   dotnet publish -c Release -f net9.0-windows10.0.19041.0
   ```

## Testing on Multiple Platforms

### Windows
- Run directly from Visual Studio or command line
- Framework: `net9.0-windows10.0.19041.0`

### macOS
- Requires macOS with Xcode installed
- Framework: `net9.0-maccatalyst`
- Run from terminal or Visual Studio for Mac

### Linux
- May require additional configuration
- Consider using Avalonia UI for better Linux support

## Troubleshooting

### API Connection Issues
- Ensure the backend API is running
- Check CORS configuration in `Program.cs`
- Verify API base URL in `ApiService.cs`

### Database Issues
- Run migrations: `dotnet ef database update`
- Check connection string in `appsettings.json`

### Build Issues
- Ensure .NET 9 SDK is installed
- Restore packages: `dotnet restore`
- Clean and rebuild: `dotnet clean && dotnet build`

## License

This project is created for educational purposes.

## Author

Development Team



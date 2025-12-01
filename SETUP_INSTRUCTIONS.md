# Setup Instructions for Procure Risk Analyzer

## Quick Start Guide

### 1. Prerequisites Installation

**Windows:**
- Install .NET 9 SDK from https://dotnet.microsoft.com/download
- Install Visual Studio 2022 with .NET MAUI workload

**macOS:**
- Install .NET 9 SDK
- Install Xcode from App Store
- Install Visual Studio for Mac or use Visual Studio Code

**Linux:**
- Install .NET 9 SDK
- Install required dependencies (varies by distribution)

### 2. Database Setup

1. Navigate to the backend project:
   ```bash
   cd ProcureRiskAnalyzer.Web
   ```

2. Install Entity Framework tools (if not already installed):
   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. Create and apply database migrations:
   ```bash
   dotnet ef migrations add AddCategoryTable
   dotnet ef database update
   ```

### 3. Running the Backend API

1. Navigate to the Web project:
   ```bash
   cd ProcureRiskAnalyzer.Web
   ```

2. Run the API:
   ```bash
   dotnet run
   ```

3. The API will be available at:
   - HTTPS: `https://localhost:7000` (or port in launchSettings.json)
   - HTTP: `http://localhost:5000`

### 4. Running the MAUI Client

#### Windows:
```bash
cd ProcureRiskAnalyzer.Client
dotnet run --framework net9.0-windows10.0.19041.0
```

#### macOS:
```bash
cd ProcureRiskAnalyzer.Client
dotnet run --framework net9.0-maccatalyst
```

#### Linux:
For Linux support, you may need to configure the project. Currently, Windows and macOS are fully supported.

### 5. Configuration

#### Backend Configuration
Edit `ProcureRiskAnalyzer.Web/appsettings.json`:
```json
{
  "DatabaseProvider": "Sqlite",
  "ConnectionStrings": {
    "Sqlite": "Data Source=procure.db"
  }
}
```

#### Client Configuration
Update the API base URL in `ProcureRiskAnalyzer.Client/Services/ApiService.cs`:
```csharp
_apiBaseUrl = "https://localhost:7000"; // Change to match your API URL
```

### 6. Testing the Application

1. **Start the backend API** (see step 3)
2. **Start the MAUI client** (see step 4)
3. **Login**: Use any username/password (demo mode)
4. **Navigate** through the application:
   - Suppliers: Create, edit, delete suppliers
   - Tenders: Create tenders linked to suppliers and categories
   - Categories: Manage tender categories
   - Statistics: View charts based on database data
   - About: View application information

### 7. Troubleshooting

**Issue: API connection fails**
- Ensure the backend is running
- Check the API URL in `ApiService.cs`
- Verify CORS is enabled in `Program.cs`

**Issue: Database errors**
- Run migrations: `dotnet ef database update`
- Check connection string in `appsettings.json`

**Issue: Build fails**
- Restore packages: `dotnet restore`
- Clean and rebuild: `dotnet clean && dotnet build`

**Issue: MAUI project won't run**
- Ensure .NET 9 SDK is installed
- Verify MAUI workload is installed: `dotnet workload install maui`
- Check platform-specific requirements (Xcode for macOS, etc.)

### 8. Building for Production

**Backend:**
```bash
cd ProcureRiskAnalyzer.Web
dotnet publish -c Release
```

**Client (Windows):**
```bash
cd ProcureRiskAnalyzer.Client
dotnet publish -c Release -f net9.0-windows10.0.19041.0
```

**Client (macOS):**
```bash
cd ProcureRiskAnalyzer.Client
dotnet publish -c Release -f net9.0-maccatalyst
```

## Features Checklist

- ✅ Cross-platform (.NET MAUI)
- ✅ MVVM pattern
- ✅ Identity Server authentication (with demo fallback)
- ✅ Animated loading screen
- ✅ Three linked database tables
- ✅ Chart visualization
- ✅ About window
- ✅ Full CRUD operations

## Next Steps

1. Set up Identity Server for production authentication
2. Configure production database (SQL Server/PostgreSQL)
3. Add unit tests
4. Configure CI/CD pipeline
5. Deploy to GitHub



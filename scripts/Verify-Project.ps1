$ErrorActionPreference = "Stop"
$projectRoot = Split-Path -Parent $PSScriptRoot
Push-Location $projectRoot
try {
    dotnet test .\tests\LifeManagementApp.Tests\LifeManagementApp.Tests.csproj
    if ($LASTEXITCODE -ne 0) { throw "Tests failed." }
    dotnet build .\src\LifeManagementApp\LifeManagementApp.csproj -f net9.0-windows10.0.19041.0
    if ($LASTEXITCODE -ne 0) { throw "Windows build failed." }
    Write-Host "Tests and Windows build passed. Run the app and complete docs/VERIFICATION.md."
}
finally { Pop-Location }

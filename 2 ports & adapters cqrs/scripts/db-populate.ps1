dotnet build 'TicketTrader.Tools.SeedDb/TicketTrader.Tools.SeedDb.csproj'

$multiplication = 20;
$app = 'TicketTrader.Tools.SeedDb\bin\Debug\netcoreapp2.0\TicketTrader.Tools.SeedDb.dll';
$files = $(Get-ChildItem resources/scenes/ -Filter *clean.indexed.svg -Recurse | % { $_.FullName });
$connectionString = 'User ID=postgres;Password=devpwd;Host=localhost;Port=5433;Database=tickettrader-event-definitions;Pooling=true';
dotnet $app $multiplication $connectionString $files
param([string]$name)
cd TicketTrader.EventDefinitions.EntityFramework
dotnet ef migrations add $name --startup-project ../TicketTrader.EventDefinitions.EntityFramework.Migrator/
cd ..
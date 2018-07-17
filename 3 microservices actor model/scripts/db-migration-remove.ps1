param([string]$name)
cd TicketTrader.EventDefinitions.EntityFramework
dotnet ef migrations remove $name --startup-project ../TicketTrader.EventDefinitions.EntityFramework.Migrator/
cd ..
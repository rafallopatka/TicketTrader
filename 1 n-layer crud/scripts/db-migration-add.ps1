param([string]$name)
cd TicketTrader.Dal
dotnet ef migrations add $name --startup-project ../TicketTrader.Dal.Migrator/
cd ..
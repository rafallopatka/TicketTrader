param([string]$name)
cd TicketTrader.Dal
dotnet ef migrations remove $name --startup-project ../TicketTrader.Dal.Migrator/
cd ..
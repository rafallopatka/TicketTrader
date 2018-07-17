#npm install -g nswag

echo "generating event definitions api clients"
cd TicketTrader.EventDefinitions.Gateway/
dotnet publish --self-contained --runtime win-x64 -c release
cd ..
cd TicketTrader.EventDefinitions.Gateway.Client/
nswag run /runtime:NetCore20
cd ..

echo "generating public api clients"
cd tickettrader.api/
dotnet publish --self-contained --runtime win-x64 -c release
cd ..
cd tickettrader.api.client/
nswag run /runtime:netcore20
cd ..
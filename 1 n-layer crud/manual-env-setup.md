
## add etc hosts

127.65.43.20   tickettrader.web
127.65.43.22   tickettrader.api
127.65.43.21   tickettrader.identity

## add port forwarding

//identity
netsh interface portproxy add v4tov4 listenport=80 listenaddress=127.65.43.21 connectport=5020 connectaddress=127.0.0.1

//api
netsh interface portproxy add v4tov4 listenport=80 listenaddress=127.65.43.22 connectport=5010 connectaddress=127.0.0.1

//web
netsh interface portproxy add v4tov4 listenport=80 listenaddress=127.65.43.20 connectport=5000 connectaddress=127.0.0.1
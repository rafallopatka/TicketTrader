
## add fiddler script
        
        if (oSession.HostnameIs("tickettrader.web")){
            oSession.host="127.0.0.1:5000";
        }
        
        if (oSession.HostnameIs("tickettrader.api")){
            oSession.host="127.0.0.1:5010";
        }
        
        if (oSession.HostnameIs("tickettrader.identity")){
            oSession.host="127.0.0.1:5020";
        }
        
        if (oSession.HostnameIs("tickettrader.management")){
            oSession.host="127.0.0.1:5030";
        }
        
        if (oSession.HostnameIs("tickettrader.bus")){
            oSession.host="127.0.0.1:15672";
        }
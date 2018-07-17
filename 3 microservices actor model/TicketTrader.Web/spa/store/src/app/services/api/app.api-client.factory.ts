
import { Injectable, Inject, Optional, OpaqueToken } from '@angular/core';
import { Http, Headers, ResponseContentType, Response } from '@angular/http';
import {
    EventsClient,
    EventsPriceZonesClient,
    EventsScenesClient,
    EventsReservationsClient,
    UsersClientsClient,
    ClientsOrdersClient,
    ClientsOrdersTicketsClient,
    ClientsOrdersReservationsClient,
    PaymentsClient,
    ClientsOrdersPaymentsClient,
    ClientsOrdersDeliveriesClient,
    DeliveriesClient,
} from 'app/services/api/app.clients';

import { environment } from 'environments/environment';
import { AuthHttp } from 'app/services/api/auth-http';

@Injectable()
export class ApiClientsFactory {
    apiAddress: string;

    constructor(private http: AuthHttp) {
        this.apiAddress = environment.apiAddress as string;
    }

    createEventsService(): EventsClient {
        return new EventsClient(this.http.asHttp(), this.apiAddress);
    }

    createEventsPriceZonesService(): EventsPriceZonesClient {
        return new EventsPriceZonesClient(this.http.asHttp(), this.apiAddress);
    }

    createEventsScenesService(): EventsScenesClient {
        return new EventsScenesClient(this.http.asHttp(), this.apiAddress);
    }

    createSeatsReservationsService(): EventsReservationsClient {
        return new EventsReservationsClient(this.http.asHttp(), this.apiAddress);
    }

    createUsersClientsService(): UsersClientsClient {
        return new UsersClientsClient(this.http.asHttp(), this.apiAddress);
    }

    createClientsOrdersService(): ClientsOrdersClient {
        return new ClientsOrdersClient(this.http.asHttp(), this.apiAddress);
    }

    createClientsOrdersReservationsService(): ClientsOrdersReservationsClient {
        return new ClientsOrdersReservationsClient(this.http.asHttp(), this.apiAddress);
    }

    createClientsOrdersTicketsService(): ClientsOrdersTicketsClient {
        return new ClientsOrdersTicketsClient(this.http.asHttp(), this.apiAddress);
    }

    createPaymentsService(): PaymentsClient {
        return new PaymentsClient(this.http.asHttp(), this.apiAddress);
    }

    createClientsOrdersPaymentsService(): ClientsOrdersPaymentsClient {
        return new ClientsOrdersPaymentsClient(this.http.asHttp(), this.apiAddress);
    }

    createDeliveriesService(): DeliveriesClient {
        return new DeliveriesClient(this.http.asHttp(), this.apiAddress);
    }

    createClientsOrdersDeliveriesService(): ClientsOrdersDeliveriesClient {
        return new ClientsOrdersDeliveriesClient(this.http.asHttp(), this.apiAddress);
    }
};

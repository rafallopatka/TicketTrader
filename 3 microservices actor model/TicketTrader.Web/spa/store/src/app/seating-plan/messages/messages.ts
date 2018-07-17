

export class EventViewVisitedMessage {
    constructor(public eventId: number) { }
}

export class SeatReservedMessage {
    constructor(public eventId: number,
                public sceneSeatId: number,
                public scenePriceZoneId: number,
                public clientId: string,
                public orderId: string) { }
}

export class SeatDiscardedMessage {
    constructor(public eventId: number, public sceneSeatIds: string[]) { }
}

export class SeatDiscardRequest {
    constructor(public eventId: number, public sceneSeatId: string) { }
}

export class TicketReservationCancelRequest {
    constructor(public eventId: number, public sceneSeatId: string) { }
}

export class TicketReserveRequest {
    constructor(public eventId: number,
        public clientId: string,
        public orderId: string,
        public sceneSeatId: number,
        public selectedOption: string,
        public priceZoneName: string,
        public optionName: string,
        public grossAmount: number) { }
}

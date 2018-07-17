

export class EventViewVisitedMessage {
    constructor(public eventId: number) { }
}

export class SeatReservedMessage {
    constructor(public eventId:
        number, public sceneSeatId:
            number, public scenePriceZoneId:
            number, public clientId: number,
        public orderId: number) { }
}

export class SeatDiscardedMessage {
    constructor(public eventId: number, public sceneSeatIds: number[]) { }
}

export class SeatDiscardRequest {
    constructor(public eventId: number, public sceneSeatId: number) { }
}

export class TicketReservationCancelRequest {
    constructor(public eventId: number, public sceneSeatId: number) { }
}

export class TicketReserveRequest {
    constructor(public eventId: number,
        public clientId: number,
        public orderId: number,
        public sceneSeatId: number,
        public selectedOption: number) { }
}

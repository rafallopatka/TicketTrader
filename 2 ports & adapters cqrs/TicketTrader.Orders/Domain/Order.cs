using System;
using System.Collections.Generic;
using System.Linq;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class Order: AggregateRoot
    {
        public Order(Id id, Client client, DateTime createDateTime, TimeSpan expirationTimeOut)
        {
            Id = id;
            Client = client;
            ExpirationTimeOut = expirationTimeOut;
            OrderStatus = Status.Active;
            Tickets = new List<Ticket>();
            Reservations = new List<Reservation>();
            CreateDateTime = createDateTime;
        }

        public Client Client { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime UpdateDateTime { get; protected set; }

        public TimeSpan ExpirationTimeOut { get; }

        public Status OrderStatus { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public List<Ticket> Tickets { get; set; }

        public List<Reservation> Reservations { get; set; }

        public void Commit()
        {
            OrderStatus = Status.Commited;
            UpdateDateTime = DateTime.Now;
        }

        public void CompleteOrder()
        {
            OrderStatus = Status.Finalized;
            UpdateDateTime = DateTime.Now;
        }

        public void Discard()
        {
            OrderStatus = Status.Canceled;
            UpdateDateTime = DateTime.Now;
        }

        public Ticket GetTicket(Id ticketId)
        {
            return Tickets.Single(x => Equals(x.Id, ticketId));
        }

        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            UpdateDateTime = DateTime.Now;
        }

        public void RemoveTicket(Ticket ticket)
        {
            Tickets.Remove(ticket);
            UpdateDateTime = DateTime.Now;
        }

        public void DicardReservation(Reservation reservation)
        {
            Reservations.Remove(reservation);
            UpdateDateTime = DateTime.Now;
        }

        public void AddReservation(Reservation reservation)
        {
            Reservations.Add(reservation);
            UpdateDateTime = DateTime.Now;
        }

        public Reservation GetReservation(Id reservationId)
        {
            return Reservations.Single(x => x.Id.Equals(reservationId));
        }

        public void SetDeliveryMethod(DeliveryMethod deliveryMethod)
        {
            DeliveryMethod = deliveryMethod;
            UpdateDateTime = DateTime.Now;
        }

        public void SetPaymentMethod(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
            UpdateDateTime = DateTime.Now;
        }

        public enum Status
        {
            Active,
            Expired,
            Commited,
            Finalized,
            Canceled
        }
    }
}

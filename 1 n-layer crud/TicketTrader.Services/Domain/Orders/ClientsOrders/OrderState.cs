namespace TicketTrader.Services.Domain.Orders.ClientsOrders
{
    public enum ClientOrderState
    {
        Active,
        Expired,
        Commited,
        Finalized,
        Canceled
    }
}
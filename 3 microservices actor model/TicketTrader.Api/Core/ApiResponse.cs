
namespace TicketTrader.Api.Core
{
    public class ApiResponse<TResult> : ApiResponse
    {
        public TResult Result { get; set; }
    }

    public class ApiResponse
    {
        public string TraceIdentifier { get; set; }
        public bool Failure { get; set; }
        public string ErrorDescription { get; set; }
    }
}
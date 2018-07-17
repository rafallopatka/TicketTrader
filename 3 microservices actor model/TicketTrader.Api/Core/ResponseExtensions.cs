using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Api.Core
{
    public static class ResponseExtensions
    {
        public static IEnumerable<TResult> AsEnumeration<TResult>(this TResult @this)
        {
            return new[] { @this };
        }

        public static async Task<IEnumerable<TResult>> AsEnumeration<TResult>(this Task<TResult> @this)
        {
            return new[] { await @this };
        }
    }
}
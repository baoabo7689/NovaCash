using System.Threading.Tasks;
using NovaCash.Sportsbook.Clients.Models;
using NovaCash.Sportsbook.Clients.Requests;
using Refit;

namespace NovaCash.Sportsbook.Clients.Services
{
    public interface ISportsbookApi
    {
        [Post("/getbetDetail")]
        Task<BetDetailResult> GetBetDetail([Body(BodySerializationMethod.UrlEncoded)]BetDetailRequest request);
    }
}
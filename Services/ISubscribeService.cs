using LandingPage.Models;
using System.Linq;
    
namespace LandingPage.Services
{
    public interface ISubscribeService
    {
        Task AddAsync(string email, string userId);
        List<Subscriber> GetUserSubscriptions(string userId);
    }

    

}

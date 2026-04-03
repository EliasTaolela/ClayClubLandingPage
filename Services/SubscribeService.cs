using LandingPage.Data;
using LandingPage.Models;
using System.Linq;

namespace LandingPage.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly AppDbContext _context;

        public SubscribeService(AppDbContext context)
        {
            _context = context;
        }

        public List<Subscriber> GetUserSubscriptions(string userId)
        {
            return _context.Subscribers
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public async Task AddAsync(string email, string userId)
        {
            var sub = new Subscriber
            {
                Email = email,
                UserId = userId
            };

            _context.Subscribers.Add(sub);
            await _context.SaveChangesAsync();
        }

        public string AddSubscriber(string email)
        {
            // Validation (C# string library)
            if (string.IsNullOrWhiteSpace(email))
                return "Email required";

            // LINQ
            var exists = _context.Subscribers
                .Any(s => s.Email == email);

            if (exists)
                return "Already subscribed";

            // Object creation
            var subscriber = new Subscriber
            {
                Email = email.Trim()
            };

            _context.Subscribers.Add(subscriber);
            _context.SaveChanges();

            return "Success";
        }
    }
}

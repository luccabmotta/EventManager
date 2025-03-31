using EventManagementAPI.Data;

namespace EventManagementAPI.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

    }
}

using DataAccess.Entities;


namespace DataAccess.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(long chatId);
    }
}

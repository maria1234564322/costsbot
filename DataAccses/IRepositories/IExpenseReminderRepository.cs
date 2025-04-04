using DataAccess.Entities;


namespace DataAccess.IRepositories
{
    public interface IExpenseReminderRepository: IRepository<User>
    {
        Task<List<User>> GetActiveUsersAsync();
        Task AddUserAsync(long userId, long сhatId);
        Task RemoveUserAsync(long userId);
    }
}

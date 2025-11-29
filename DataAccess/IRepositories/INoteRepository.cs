using DataAccess.Entities;

namespace DataAccess.IRepositories
{
    public interface INoteRepository
    {
        void Add(Note note);
        void Update(Note note);
        void Remove(Note note);
        Note GetById(int id);
        List<Note> GetAll();
        void SaveChanges();
    }
}

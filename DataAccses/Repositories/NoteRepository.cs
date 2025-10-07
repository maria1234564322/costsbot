
using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;

        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Note note)
        {
            _context.Notes.Add(note);
        }

        public void Update(Note note)
        {
            _context.Notes.Update(note);
        }

        public void Remove(Note note)
        {
            _context.Notes.Remove(note);
        }

        public Note GetById(int id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }

        public List<Note> GetAll()
        {
            return _context.Notes.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}

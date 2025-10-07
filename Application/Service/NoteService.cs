using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;


namespace Application.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void AddNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            _noteRepository.Add(note);
            _noteRepository.SaveChanges();
        }

        public void UpdateNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            _noteRepository.Update(note);
            _noteRepository.SaveChanges();
        }

        public void DeleteNote(int id)
        {
            var note = _noteRepository.GetById(id);
            if (note == null)
                throw new InvalidOperationException($"Note with ID {id} not found.");

            _noteRepository.Remove(note);
            _noteRepository.SaveChanges();
        }

        public List<Note> GetAllNotes()
        {
            return _noteRepository.GetAll();
        }

        public Note GetNoteById(int id)
        {
            return _noteRepository.GetById(id);
        }
    }
}

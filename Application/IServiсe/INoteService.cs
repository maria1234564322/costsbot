using DataAccess.Entities;


namespace Application.IServiсe
{
    public interface INoteService
    {
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(int id);
        List<Note> GetAllNotes();
        Note GetNoteById(int id);
    }
}

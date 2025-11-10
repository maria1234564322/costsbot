using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;


namespace Application.Service
{
    public class DayMenuService : IDayMenuService
    {
        private readonly IDayMenuRepository _dayMenuRepository;
        public DayMenuService(IDayMenuRepository dayMenuRepository)
        {
            _dayMenuRepository = dayMenuRepository;
        }
        public void AddDayMenu(DayMenu dayMenu)
        {
            if (dayMenu == null)
            {
                throw new ArgumentNullException(nameof(dayMenu));
            }

            _dayMenuRepository.Add(dayMenu);
        }

        public void DeleteDayMenu(int id)
        {
            var note = _dayMenuRepository.GetById(id);
            if (note == null)
                throw new InvalidOperationException($"Note with ID {id} not found.");

            _dayMenuRepository.Remove(note);
            _dayMenuRepository.SaveChanges();
        }

        public List<DayMenu> GetAllDayMenus()
        {
            return _dayMenuRepository.GetAll();
        }

        public DayMenu GetDayMenuById(int id)
        {
            var dayMenu = _dayMenuRepository.GetById(id);
            if (dayMenu == null)
                throw new InvalidOperationException($"DayMenu with ID {id} not found.");

            return dayMenu;
        }

        public void UpdateDayMenu(DayMenu dayMenu)
        {
            if (dayMenu == null)
                throw new ArgumentNullException(nameof(dayMenu));

            var existing = _dayMenuRepository.GetById(dayMenu.Id);
            if (existing == null)
                throw new InvalidOperationException($"DayMenu with ID {dayMenu.Id} not found.");

            _dayMenuRepository.Update(dayMenu);
            _dayMenuRepository.SaveChanges();
        }
    }
}

using DataAccess;
using DataAccess.Entities;

namespace Application.IServiсe
{
    public interface IDayMenuService
    {
        void AddDayMenu(DayMenu dayMenu);
        void UpdateDayMenu(DayMenu dayMenu);
        void DeleteDayMenu(int id);
        List<DayMenu> GetAllDayMenus();
        DayMenu GetDayMenuById(int id);

    }
}

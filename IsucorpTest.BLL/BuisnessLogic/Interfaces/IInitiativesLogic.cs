using GestionAvanceProyecto.DAL.Repositories.Interfaces;
using GestionAvanceProyecto.Model.DBModel;
using GestionAvanceProyecto.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAvanceProyecto.BLL.BuisnessLogic.Interfaces
{
    public interface IInitiativesLogic
    {
        List<InitiativeViewModel> GetInitiatives();
        List<InitiativeViewModel> GetUserInitiatives(string userId);
        Task<List<InitiativeViewModel>> GetInitiativesByUserRole(string userId);
        InitiativeViewModel GetInitiativeById(int id);
        int AddInitiative(InitiativeViewModel initiative);
        InitiativeViewModel GetNewInitiative();
        InitiativeViewModel UpdateInitiative(InitiativeViewModel initiative);
        bool DeleteInitiative(int id);

    }
}

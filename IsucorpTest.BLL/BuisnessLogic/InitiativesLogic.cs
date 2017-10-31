using GestionAvanceProyecto.BLL.BuisnessLogic.Interfaces;
using GestionAvanceProyecto.Core;
using GestionAvanceProyecto.DAL.Repositories.Interfaces;
using GestionAvanceProyecto.Model.DBModel;
using GestionAvanceProyecto.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAvanceProyecto.BLL.BuisnessLogic
{
    public class InitiativesLogic : IInitiativesLogic
    {
        private ICollectionRepository<Initiative> initiativesRepository;
        private ICollectionRepository<KPI> kpisRepository;
        private ICollectionRepository<Milestone> milestonesRepository;
        private ICollectionRepository<ImpactYear> impactYearsRepository;
        private IAuthLogic authLogic;

        public InitiativesLogic(ICollectionRepository<Initiative> initiativesRepository, ICollectionRepository<KPI> kpisRepository, ICollectionRepository<Milestone> milestonesRepository, ICollectionRepository<ImpactYear> impactYearsRepository, IAuthLogic authLogic)
        {
            this.initiativesRepository = initiativesRepository;
            this.kpisRepository = kpisRepository;
            this.milestonesRepository = milestonesRepository;
            this.impactYearsRepository = impactYearsRepository;
            this.authLogic = authLogic;
        }

        public List<InitiativeViewModel> GetInitiatives()
        {
            return initiativesRepository.List().Select(i => new InitiativeViewModel(i)).ToList();
        }

        public List<InitiativeViewModel> GetUserInitiatives(string userId)
        {
            return initiativesRepository.List(i => i.CreatorId == userId).Select(i => new InitiativeViewModel(i)).ToList();
        }

        public async Task<List<InitiativeViewModel>> GetInitiativesByUserRole(string userId)
        {
            if (await authLogic.IsUserInRole(userId, Constants.RoleName_Admin))
                return GetInitiatives();
            else if (await authLogic.IsUserInRole(userId, Constants.RoleName_User))
                return GetUserInitiatives(userId);
            else if(await authLogic.IsUserInRole(userId, Constants.RoleName_Viewer))
                return GetInitiatives();

            return new List<InitiativeViewModel>();
        }

        public InitiativeViewModel GetInitiativeById(int id)
        {
            return initiativesRepository.List(i => i.Id == id).Select(i => new InitiativeViewModel(i)).FirstOrDefault();
        }

        public int AddInitiative(InitiativeViewModel initiative)
        {
            var newInitative = new Initiative(initiative);

            return initiativesRepository.Add(newInitative);
        }

        public InitiativeViewModel GetNewInitiative()
        {
            InitiativeViewModel newInit = new InitiativeViewModel();
            newInit.ImpactYears = new List<ImpactYearViewModel>();

            for (int i = 1; i < 5; i++)
            {
                int newDate = (DateTime.Now.Year) + i;
                ImpactYear impY = new ImpactYear("FY" + newDate.ToString(), newDate);
                newInit.ImpactYears.Add(new ImpactYearViewModel(impY));
            }

            newInit.KPIs = new List<KPIViewModel>();
            newInit.Milestones = new List<MilestoneViewModel>();
            return newInit;
        }

        public InitiativeViewModel UpdateInitiative(InitiativeViewModel initiative)
        {
            var edit = new Initiative(initiative);

            foreach (var year in edit.ImpactYears)
            {
                year.Initiative = null;
                year.InitiativeId = edit.Id;
                impactYearsRepository.Update(year);
            }

            UpdateKPIs(edit);
            UpdateMilestones(edit);

            initiativesRepository.Update(edit);

            return GetInitiativeById(edit.Id);
        }

        private void UpdateKPIs(Initiative initiative)
        {
            // Existing kpis ids
            var currentKpisId = kpisRepository.List(k => k.InitiativeId == initiative.Id).Select(k => k.Id).ToList();

            // New set of kpis asociated to the Initiative
            var newKpisId = initiative.KPIs != null ? initiative.KPIs.Select(k => k.Id) : new List<int>();

            // Current existing KPIs that no longer are in the Initiative
            var kpisToDelete = kpisRepository.List(k => k.InitiativeId == initiative.Id && !newKpisId.Contains(k.Id)).ToList();
            // New KPIs that doesn't exists in the database
            var kpisToAdd = initiative.KPIs != null ? initiative.KPIs.Where(k => !currentKpisId.Contains(k.Id)).ToList() : new List<KPI>();

            var kpisToUpdate = initiative.KPIs != null ? initiative.KPIs.Where(k => currentKpisId.Contains(k.Id)).ToList() : new List<KPI>();


            foreach (var kpi in kpisToDelete)
            {
                kpisRepository.Delete(kpi.Id);
            }

            foreach (var kpi in kpisToAdd)
            {
                kpi.InitiativeId = initiative.Id;
                kpisRepository.Add(kpi);
            }

            foreach (var kpi in kpisToUpdate)
            {
                kpi.InitiativeId = initiative.Id;
                kpi.Initiative = null;
                kpisRepository.Update(kpi);
            }

        }

        private void UpdateMilestones(Initiative initiative)
        {
            // Existing Milestones ids
            var currentMilestonesId = milestonesRepository.List(m => m.InitiativeId == initiative.Id).Select(m => m.Id).ToList();

            // New set of Milestones asociated to the Initiative
            var newMilestonesId = initiative.Milestones != null ? initiative.Milestones.Select(m => m.Id) : new List<int>();

            // Current existing Milestones that no longer are in the Initiative
            var milestonesToDelete = milestonesRepository.List(m => m.InitiativeId == initiative.Id && !newMilestonesId.Contains(m.Id)).ToList();
            // New Milestones that doesn't exists in the database
            var milestonesToAdd = initiative.Milestones != null ? initiative.Milestones.Where(m => !currentMilestonesId.Contains(m.Id)).ToList() : new List<Milestone>();

            var milestonesToUpdate = initiative.Milestones != null ? initiative.Milestones.Where(m => currentMilestonesId.Contains(m.Id)).ToList() : new List<Milestone>();

            foreach (var milestone in milestonesToDelete)
            {
                milestonesRepository.Delete(milestone.Id);
            }

            foreach (var milestone in milestonesToAdd)
            {
                milestone.InitiativeId = initiative.Id;
                milestonesRepository.Add(milestone);
            }

            foreach (var milestone in milestonesToUpdate)
            {
                milestone.InitiativeId = initiative.Id;
                milestone.Initiative = null;
                milestonesRepository.Update(milestone);
            }
        }

        public bool DeleteInitiative(int id)
        {
            try
            {
                var initiative = initiativesRepository.FindById(id);
                initiativesRepository.Delete(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

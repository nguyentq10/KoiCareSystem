using Repositories.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PondService
    {
        private PondRepository _repo = new();

        public List<Pond> GetAllPonds()
        {
            return _repo.GetAllPonds();
        }

        public Pond? GetPondById(int pondId)
        {
            return _repo.GetPondById(pondId);
        }

        public Pond? GetPondByUserId(string userId)
        {
            return _repo.GetPondByUserId(userId);
        }

        public List<Pond>? GetPondsByUserId(string userId)
        {
            return _repo.GetPondsByUserId(userId);
        }

        public void AddPond(Pond pond)
        {
            _repo.AddPond(pond);
        }

        public void UpdatePond(Pond pond)
        {
            _repo.UpdatePond(pond);
        }

        public void DeletePond(Pond pond)
        {
            try
            {
                _repo.DeletePond(pond);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the pond: {ex.Message}", ex);
            }
        }
    }
}

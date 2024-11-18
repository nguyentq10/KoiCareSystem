using Repositories.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class KoiService
    {
        private KoiRepository _repo = new();

        public List<Koi> GetAllKois()
        {
            return _repo.GetAllKois();
        }

        public List<Koi>? GetKoisByPondId(int pondId)
        {
            return _repo.GetKoisByPondId(pondId);
        }

        public List<Koi>? GetKoisByUserId(string userId)
        {
            return _repo.GetKoisByUserId(userId);
        }

        public void AddKoi(Koi kois) {
            _repo.AddKoi(kois);
        }

        public void UpdateKoi(Koi kois)
        {
            _repo.UpdateKoi(kois);
        }

        public void DeleteKoi(Koi kois)
        {
            _repo.DeleteKoi(kois);
        }
    }
}

using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class PondRepository
    {
        private KoiCareSystemContext _context;

        public List<Pond> GetAllPonds()
        {
            _context = new();
            return _context.Ponds.ToList();
        }

        public Pond? GetPondById(int pondId)
        {
            _context = new();
            return _context.Ponds.FirstOrDefault(p => p.PondId == pondId);
        }

        public Pond? GetPondByUserId(string userId)
        {
            _context = new();
            return _context.Ponds.FirstOrDefault(p => p.UserId == userId);
        }

        public List<Pond>? GetPondsByUserId(string userId)
        {
            _context = new();
            return _context.Ponds.Where(p => p.UserId == userId).ToList();
        }

        public void AddPond(Pond pond)
        {
            _context = new();
            _context.Ponds.Add(pond);
            _context.SaveChanges();
        }

        public void UpdatePond(Pond pond)
        {
            _context = new();
            _context.Ponds.Update(pond);
            _context.SaveChanges();
        }

        public void DeletePond(Pond pond)
        {
            _context = new();
            _context.Ponds.Remove(pond);
            _context.SaveChanges();
        }
    }
}

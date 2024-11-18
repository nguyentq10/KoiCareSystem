using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MeasurementRepo
    {
        private KoiCareSystemContext? _context;

        public List<Measurement> GetAllKois()
        {
            _context = new();
            return _context.Measurements.ToList();
        }

        public List<Measurement>? GetMeasurementsByPondId(int pondId)
        {
            _context = new();
            return _context.Measurements.Where(k => k.PondId == pondId).ToList();
        }

        public List<Measurement>? GetKoisByUserId(string userId)
        {
            _context = new();
            return _context.Measurements.Where(k => k.UserId == userId).ToList();
        }

        public void AddKoi(Measurement koi)
        {
            _context = new();
            _context.Measurements.Add(koi);
            _context.SaveChanges();
        }

        public void UpdateKoi(Measurement koi)
        {
            _context = new();
            _context.Measurements.Update(koi);
            _context.SaveChanges();
        }

        public void DeleteKoi(Measurement koi)
        {
            _context = new();
            _context.Measurements.Remove(koi);
            _context.SaveChanges();
        }

    }
}

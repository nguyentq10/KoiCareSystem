using Repositories.Entities;
using Repositories.Repositories;
using System.Collections.Generic;

namespace Services.Services
{
    public class MeasurementService
    {
        private readonly MeasurementRepo _repo = new();

        public List<Measurement> GetAllMeasurement()
        {
            return _repo.GetAllKois();
        }

        public List<Measurement>? GetMeasurementsByPondId(int pondId)
        {
            return _repo.GetMeasurementsByPondId(pondId);
        }

        public List<Measurement>? GetMeasurementsByUserId(string userId)
        {
            return _repo.GetKoisByUserId(userId);
        }

        public void AddMeasurement(Measurement kois)
        {
            _repo.AddKoi(kois);
        }

        public void UpdateMeasurement(Measurement kois)
        {
            _repo.UpdateKoi(kois);
        }

        public void DeleteMeasurement(Measurement kois)
        {
            _repo.DeleteKoi(kois);
        }
    }
}

using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class MeasurementUnitsRepository : IRepository<MeasurementUnit>
    {
        private IDatabase db = DatabaseConnection.Instance;
        public IList<MeasurementUnit> GetAllRecords()
        {
            return db.Fetch<MeasurementUnit>(SqlConstants.ALL_MEASUREMENT_UNITS);
        }

        public MeasurementUnit GetRecordById(int id)
        {
            return db.SingleById<MeasurementUnit>(id);
        }

        public MeasurementUnit GetRecordByName(string name)
        {
            return db.Fetch<MeasurementUnit>(SqlConstants.MEASUREMENT_UNIT_BY_NAME(name)).First();
        }

        public void AddRecord(MeasurementUnit record)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, MeasurementUnit record)
        {
            throw new NotImplementedException();
        }
    }
}

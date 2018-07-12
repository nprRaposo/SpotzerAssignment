using SpotzerAssignment.Model;
using System;

namespace SpotzerAssignment.Data
{
    public class PaidSearchProductRepository : IRepository<PaidSearchProductLine>
    {
        private readonly SpotzerContext _context;

        public PaidSearchProductRepository(SpotzerContext context)
        {
            this._context = context;
        }

        public void Save(PaidSearchProductLine entity)
        {
            this._context.PaidSearchProductLine.Add(entity);
            this._context.SaveChanges();
        }
    }
}

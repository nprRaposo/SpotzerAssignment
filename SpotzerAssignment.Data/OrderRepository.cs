using SpotzerAssignment.Model;
using System;

namespace SpotzerAssignment.Data
{
    public class OrderRepository: IRepository<Order>
    {
        private readonly SpotzerContext _context;

        public OrderRepository(SpotzerContext context)
        {
            this._context = context;
        }

        public void Save(Order entity)
        {
            this._context.Orders.Add(entity);
            this._context.SaveChanges();
        }
    }
}

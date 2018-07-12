using SpotzerAssignment.Model;
using System;

namespace SpotzerAssignment.Data
{
    public class WebSiteProductRepository : IRepository<WebSiteProductLine>
    {
        private readonly SpotzerContext _context;

        public WebSiteProductRepository(SpotzerContext context)
        {
            this._context = context;
        }

        public void Save(WebSiteProductLine entity)
        {
            this._context.WebSiteProductLine.Add(entity);
            this._context.SaveChanges();
        }
    }
}

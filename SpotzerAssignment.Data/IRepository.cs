using System;
using System.Collections.Generic;
using System.Text;

namespace SpotzerAssignment.Data
{
    interface IRepository <T>
    {
        void Save(T entity);
    }
}

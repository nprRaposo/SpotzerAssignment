using System;
using System.Collections.Generic;
using System.Text;

namespace SpotzerAssignment.Service
{
    interface IService<T>
    {
        void Save(T entity);
    }
}

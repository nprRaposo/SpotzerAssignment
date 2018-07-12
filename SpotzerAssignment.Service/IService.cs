using System;
using System.Collections.Generic;
using System.Text;

namespace SpotzerAssignment.Service
{
    public interface IService<T>
    {
        void Save(T entity);
    }
}

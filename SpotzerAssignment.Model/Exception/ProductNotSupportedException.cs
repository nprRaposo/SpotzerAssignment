using System;
using System.Collections.Generic;
using System.Text;

namespace SpotzerAssignment.Model.Exception
{
    public class ProductNotSupportedException : ApplicationException
    {
        public ProductNotSupportedException(string message) : base(message) { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpotzerAssignment.Model
{
    public class Partner
    {
        public string PartnerId { get; set; }

        public IEnumerable<ProductTypeEnum> SupportedSellings { get; set; }
    }
}

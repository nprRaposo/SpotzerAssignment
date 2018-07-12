using System;
using System.Collections.Generic;
using System.Text;

namespace SpotzerAssignment.Model.DTO
{
    public class OrderDTO
    {
        public string Partner { get; set; }
        public string OrderId { get; set; }
        public string TypeOfOder { get; set; }
        public string SubmittedBy { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactTitle { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string ExposureID { get; set; }
        public string UDAC { get; set; }
        public string RelatedOrder { get; set; }

        public IEnumerable<LineDTO> LineItems { get; set; }
    }
}

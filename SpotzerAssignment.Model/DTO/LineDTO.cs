using System;
using System.Collections.Generic;
using System.Text;

namespace SpotzerAssignment.Model.DTO
{
    public class LineDTO
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductType { get; set; }
        public string Notes { get; set; }
        public string Category { get; set; }

        public PaidSearchProductLine AdWordCampaign { get; set; }

        public WebSiteProductLine WebsiteDetails { get; set; }
    }
}

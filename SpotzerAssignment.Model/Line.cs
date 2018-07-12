using System;
using System.ComponentModel.DataAnnotations;

namespace SpotzerAssignment.Model
{
    public abstract class Line
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductType { get; set; }
        public string Notes { get; set; }
        public string Category { get; set; }

        public Order Order { get; set; }
    }
}

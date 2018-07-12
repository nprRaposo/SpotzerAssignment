using System;

namespace SpotzerAssignment.Model
{
    public abstract class Line
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductType { get; set; }
        public string Notes { get; set; }
        public string Category { get; set; }
    }
}

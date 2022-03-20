using System.ComponentModel.DataAnnotations.Schema;

namespace evoWebAPI.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public int RG { get; set; }
        public string picture { get; set; }
        public string pictureName { get; set; } 
        public int departmentId { get; set; }
        public Department department { get; set; }
    }
}

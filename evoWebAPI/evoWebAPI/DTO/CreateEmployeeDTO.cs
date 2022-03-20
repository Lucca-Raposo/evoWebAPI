using System.ComponentModel.DataAnnotations;

namespace evoWebAPI.DTO
{
    public class CreateEmployeeDTO
    {
        [Required]
        public string name { get; set; }

        [Required]
        public int RG { get; set; }

        [Required]
        public int departmentId { get; set; }
    }
}

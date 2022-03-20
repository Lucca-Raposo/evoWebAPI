using System.ComponentModel.DataAnnotations;

namespace evoWebAPI.DTO
{
    public class CreateDepartmentDTO
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string abbrev { get; set; }
    }
}

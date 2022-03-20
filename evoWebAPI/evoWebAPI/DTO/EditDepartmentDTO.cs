using System.ComponentModel.DataAnnotations;

namespace evoWebAPI.DTO
{
    public class EditDepartmentDTO
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string abbrev { get; set; }
    }
}

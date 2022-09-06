using System.ComponentModel.DataAnnotations;

namespace PersonApp.DTO
{
    public class PersonDto
    {
        [Required(ErrorMessage = "Person FirstName is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Person LastName is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the LastName is 20 characters.")]
        public string LastName { get; set; }
    }
}

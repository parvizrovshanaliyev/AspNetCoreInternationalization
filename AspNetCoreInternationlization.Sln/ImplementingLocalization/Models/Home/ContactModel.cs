using System.ComponentModel.DataAnnotations;

namespace ImplementingLocalization.Models.Home
{
    public class ContactModel
    {
        [Required(ErrorMessage = "RequiredError")]
        [MaxLength(50,ErrorMessage = "MaxLengthError")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredError")]
        [MaxLength(500, ErrorMessage = "MaxLengthError")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}

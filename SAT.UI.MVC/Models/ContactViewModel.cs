using System.ComponentModel.DataAnnotations;

namespace SAT.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "*Required")]
        [DataType(DataType.EmailAddress)]//enforces standard emil formatting
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "*Required")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "*Required")]
        [DataType(DataType.MultilineText)]//gives us a larger textbox (textarea in HTML)
        public string Message { get; set; } = null!;
    }

}

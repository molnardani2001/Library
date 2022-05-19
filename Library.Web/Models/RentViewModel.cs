using System.ComponentModel.DataAnnotations;
using Library.Data;

namespace Library.Web.Models
{
    public class RentViewModel
    {
        public Volume? Volume { get; set; }

        public Book? Book { get; set; }

        [Required(ErrorMessage = "A kezdő dátum megadása kötelező.")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "A vége dátum megadása kötelező.")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
    }
}

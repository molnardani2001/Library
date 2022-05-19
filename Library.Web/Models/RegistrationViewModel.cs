using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "A név megadása kötelező.")]
        [StringLength(60, ErrorMessage = "A név maximum 60 karakter hosszú lehet.")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage ="E-mail cím mgadása kötelező.")]
        [EmailAddress(ErrorMessage ="Az e-mail cím nem megfelelő formátumú.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "A telefonszám megadása kötelező.")]
        [Phone(ErrorMessage = "A telefonszám formátuma nem megfelelő.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;


        [Required(ErrorMessage = "A felhasználónév megadása kötelező.")]
        [RegularExpression("^[A-Za-z0-9_-]{5,40}", ErrorMessage = "A felhasználónév formátuma, vagy hossza nem megfelelő.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "A jelszó megadása kötelező.")]
        [RegularExpression("^[A-Za-z0-9_-]{5,40}", ErrorMessage = "A jelszó formátuma, vagy hossza nem megfelelő.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "A jelszó ismételt megadása kötelező.")]
        [Compare(nameof(Password), ErrorMessage = "A két jelszó nem egyezik.")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; } = null!;
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Visitor : IdentityUser<int>
    {
        public Visitor()
        {
            Rents = new HashSet<Rent>();
        }
        //[Key]
        //public int Id { get; set; }

        //public string UserName { get; set; } = null!;

        public string Name { get; set; } = null!;

        //public string Email { get; set; } = null!;

        //public string PhoneNumber { get; set; } = null!;

        //public byte[] Password { get; set; } = null!;

        //public string UserChallenge { get; set; } = null!;

        public ICollection<Rent> Rents { get; set; }

    }
}

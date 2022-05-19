using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Library.Data
{
    public class Librarian : IdentityUser<int>
    {
        //[Key]
        //public int Id { get; set; }

        public string Name { get; set; } = null!;

        //public string UserName { get; set; } = null!;

        //public byte[] Password { get; set; } = null!;
    }
}

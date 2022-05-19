using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Volume
    {
        public Volume()
        {
            Rents = new HashSet<Rent>();
        }

        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public ICollection<Rent> Rents { get; set; }
    }
}

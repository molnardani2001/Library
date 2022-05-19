using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Book
    {
        public Book()
        {
            Volumes = new HashSet<Volume>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Writer { get; set; } = null!;

        public int Year { get; set; }

        public string ISBNNumber { get; set; } = null!;

        public int PopularityNumber { get; set; }

        public byte[] SmallCoverImage { get; set; } = null!;

        public byte[] NormalCoverImage { get; set; } = null!;

        public ICollection<Volume> Volumes { get; set; }
    }
}

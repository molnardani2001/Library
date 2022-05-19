using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class BookDTO
    {
        public Int32 Id { get; set; }

        public string Title { get; set; } = null!;

        public string Writer { get; set; } = null!;

        public Int32 Year { get; set; }

        public string ISBNNumber { get; set; } = null!;

        public Int32 PopularityNumber { get; set; }

        public byte[] SmallCoverImage { get; set; } = null!;

        public byte[] NormalCoverImage { get; set; } = null!;


    }
}

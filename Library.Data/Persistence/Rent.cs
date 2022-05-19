using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Volume")]
        public int VolumeId { get; set; }

        public Volume Volume { get; set; } = null!;

        [ForeignKey("Visitor")]
        public int VisitorId { get; set; }

        public Visitor Visitor { get; set; } = null!;

        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }

        public Boolean IsActive { get; set; }

        public Boolean IsConflict(DateTime start, DateTime end)
        {
            return Start >= start && Start < end ||
                   End >= start && End < end ||
                   Start < start && End > end ||
                   Start > start && End < end;
        }

    }
}

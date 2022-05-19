using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class RentDTO
    {
        public int Id { get; set; }

        public int VolumeId { get; set; }

        public int VisitorId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Boolean IsActive { get; set; }

    }
}

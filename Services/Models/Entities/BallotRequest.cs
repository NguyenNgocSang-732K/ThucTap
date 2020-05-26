using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models.Entities
{
    public class BallotRequest
    {
        public int Id { get; set; }
        public int AccId { get; set; }
        public bool Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DayCreate { get; set; }
        public int Status { get; set; }
        public string AccName { get; set; }
        public bool Active { get; set; }
    }
}

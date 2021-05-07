using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_cinema
{
    public partial class Film
    {
        public Film()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int FilmsId { get; set; }
        public string FilmsName { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public TimeSpan? Time { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

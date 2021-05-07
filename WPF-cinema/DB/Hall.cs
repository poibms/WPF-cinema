using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_cinema
{
    public partial class Hall
    {
        public Hall()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int HallsId { get; set; }
        public string HallsName { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

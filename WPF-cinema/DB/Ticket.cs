using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_cinema
{
    public partial class Ticket
    {
        public Ticket()
        {
            OrderTickets = new HashSet<OrderTicket>();
        }

        public int TicketsId { get; set; }
        public int? HallsId { get; set; }
        public int? FilmsId { get; set; }
        public int Place { get; set; }

        public virtual Film Films { get; set; }
        public virtual Hall Halls { get; set; }
        public virtual ICollection<OrderTicket> OrderTickets { get; set; }
    }
}

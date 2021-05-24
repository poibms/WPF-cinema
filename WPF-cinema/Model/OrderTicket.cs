using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_cinema
{
    public partial class OrderTicket
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TicketsId { get; set; }

        public virtual Ticket Tickets { get; set; }
        public virtual User User { get; set; }
    }
}

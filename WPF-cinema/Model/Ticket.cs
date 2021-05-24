using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WPF_cinema
{
    public partial class Ticket
    {
        public Ticket()
        {
            OrderTickets = new HashSet<OrderTicket>();
        }

        public Ticket(int row, int place)
        {
            Row = row;
            Place = place;
            Session = new Session();
            OrderTickets = new List<OrderTicket>();
        }

        public int TicketsId { get; set; }

        //4[ForeignKey("Session")]
        public int? SessionId { get; set; }
        public int Row { get; set; }
        public int Place { get; set; }

        public virtual Session Session { get; set; }
        public virtual ICollection<OrderTicket> OrderTickets { get; set; }

      
    }
}

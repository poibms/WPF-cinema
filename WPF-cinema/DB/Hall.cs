using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_cinema
{
    public partial class Hall
    {
        public Hall()
        {
            Sessions = new HashSet<Session>();
        }

        public int HallsId { get; set; }
        public string HallsName { get; set; }
        public int Capacity { get; set; }

        public Hall(string HallsName, int Capacity)
        {
            this.HallsName = HallsName;
            this.Capacity = Capacity;
        }
        public override string ToString()
        {
            return HallsName;
        }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}

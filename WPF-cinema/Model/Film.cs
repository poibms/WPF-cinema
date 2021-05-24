using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WPF_cinema
{
    public partial class Film
    {
        public Film()
        {
            Sessions = new HashSet<Session>();
        }

        public int FilmsId { get; set; }
        public string FilmsName { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Time { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }

        public Film(string filmName, string genre, string country, string director, string time, string description)
        {
            this.FilmsName = filmName;
            this.Genre = genre;
            this.Country = country;
            this.Director = director;
            this.Time = time;
            this.Description = description;

        }
        public override string ToString()
        {
            return FilmsName;
        }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}

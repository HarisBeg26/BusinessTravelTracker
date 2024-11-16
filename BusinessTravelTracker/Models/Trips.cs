using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Supabase.Postgrest.Attributes;

namespace BusinessTravelTracker.Models
{
    [Table("trips")]
    public class Trips :SupabaseModel
    {
        [PrimaryKey("id", false)] // Primary key column
        public int Id { get; set; }

        [Column("destination")]
        public string Destination { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("purpose")]
        public string Purpose { get; set; }
    }
}

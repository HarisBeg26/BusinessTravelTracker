using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Supabase.Postgrest.Attributes;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;

namespace BusinessTravelTracker.Models
{
    [Supabase.Postgrest.Attributes.Table("expenses")]
    public class Expense : SupabaseModel
    {
        [PrimaryKey("id", false)] // Primary key column
        public int Id { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("description")]
        public string Description { get; set; }
        [Column("category")]
        public string Category { get; set; }

        [Column("trip_id")]
        public int TripId { get; set; } // Foreign key to the trips table
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models
{
    public class Bookloan
    {
        [Key]
        public int LoanId { get; set; }
        public int? InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // computed property. Räknas ut from andra properties
        // saknas setter så kommer EF ignorera vid add migrations
        public bool Returned
        {
            get
            {
                // är return inte null så betyder det att filmen inte är återlämnad
                return ReturnDate != null;
            }
        }
    }
}

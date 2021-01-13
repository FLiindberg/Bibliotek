using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public List<Bookloan> Bookloans { get; set; }
        // computed property. Räknas ut from andra properties
        // saknas setter så kommer EF ignorera vid add migrations
        public bool Available
        {
            get
            {
                // ifall den skulle vara null är den ny. Då är den tillgänglig!
                if (Bookloans == null)
                    return true;
                // ifall den aldrig hyrts ut är den tillgänglig!
                else if (Bookloans.Count == 0)
                    return true;
                // har alla filmer lämnats tillbaka är den tillgänglig
                else if (Bookloans.All(r => r.Returned))
                    return true;
                // annars är den inte tillgänglig
                else
                {
                    return false;
                }
            }
        }
    }
}

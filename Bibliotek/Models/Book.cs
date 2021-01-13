using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public int Isbn { get; set; }
        public int ReleaseYear { get; set; }
        public int Rating { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Library_Management_System
{
    internal class BorrowingHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("patron")]
        public int PatronId { get; set; }

     
        public Patron patron { get; set; }

        [Required]
        [ForeignKey("book")]
        public int BookId { get; set; }

       
        public Book book { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Library_Management_System
{
    internal class BookOperation
    {
        public void AddBook(string title, string author, int publicationYear)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var newBook = new Book
                {
                    Title = title,
                    Author = author,
                    PublicationYear = publicationYear,
                    IsAvailable = true 
                };

                dbContext.Books.Add(newBook);
                dbContext.SaveChanges();

                Console.WriteLine("Book added successfully to the library.");
            }
        }
    }
}


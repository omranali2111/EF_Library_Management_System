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
                if (dbContext.Books.Any(b => b.Title == title && b.Author == author))
                {
                    Console.WriteLine("The book with the same title and author already exists in the library.");
                    return;
                }
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
        public void RemoveBook(string title)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var bookToRemove = dbContext.Books.FirstOrDefault(book => book.Title == title);
                if (bookToRemove != null)
                {
                    // Remove the book from the context and save changes
                    dbContext.Books.Remove(bookToRemove);
                    dbContext.SaveChanges();
                    Console.WriteLine($"Book '{title}' has been removed from the library.");
                }
                else
                {
                    Console.WriteLine($"Book '{title}' was not found in the library.");
                }

            }
        }
    }
}


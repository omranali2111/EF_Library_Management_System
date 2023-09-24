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

        public void UpdateBook(int bookId, string newTitle, string newAuthor, int newPublicationYear)
        {
            using (var dbContext = new LibraryDBContext())
            {
                // Find the book by its ID
                var bookToUpdate = dbContext.Books.FirstOrDefault(book => book.Id == bookId);

                if (bookToUpdate != null)
                {
                    // Update the book's properties
                    bookToUpdate.Title = newTitle;
                    bookToUpdate.Author = newAuthor;
                    bookToUpdate.PublicationYear = newPublicationYear;

                    // Save the changes to the database
                    dbContext.SaveChanges();

                    Console.WriteLine($"Book with ID {bookId} has been updated.");
                }
                else
                {
                    Console.WriteLine($"Book with ID {bookId} was not found.");
                }
            }
        }

        public void ViewAllBooks()
        {
            using (var dbContext = new LibraryDBContext())
            {
                var books = dbContext.Books.ToList();

                if (books.Count > 0)
                {
                    Console.WriteLine("All Books in the Library:");
                    Console.WriteLine("---------------------------");

                    foreach (var book in books)
                    {
                        Console.WriteLine($"ID: {book.Id}");
                        Console.WriteLine($"Title: {book.Title}");
                        Console.WriteLine($"Author: {book.Author}");
                        Console.WriteLine($"Publication Year: {book.PublicationYear}");
                        Console.WriteLine($"Availability: {(book.IsAvailable ? "Available" : "Not Available")}");
                        Console.WriteLine("---------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No books found in the library.");
                }
            }
        }


    }
}


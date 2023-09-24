using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Library_Management_System
{
    internal class PatronOperation
    {
        public void AddPatron(string name, string contactInfo)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var newPatron = new Patron
                {
                    Name = name,
                    ContactNumber = contactInfo
                };

                dbContext.Patrons.Add(newPatron);
                dbContext.SaveChanges();

                Console.WriteLine("Patron added successfully to the library.");
            }
        }
        public void RemoveBook(string title)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var bookToRemove = dbContext.Books.FirstOrDefault(book => book.Title == title);
                if (bookToRemove != null)
                {
                    // Set the IsAvailable property of the book to false
                    bookToRemove.IsAvailable = false;
                    dbContext.SaveChanges();
                    Console.WriteLine($"Book '{title}' has been marked as unavailable in the library.");
                }
                else
                {
                    Console.WriteLine($"Book '{title}' was not found in the library.");
                }
            }
        }
        public void UpdatePatron(int patronId, string name, string contactInformation)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var patronToUpdate = dbContext.Patrons.FirstOrDefault(patron => patron.Id == patronId);

                if (patronToUpdate != null)
                {
                    // Update the patron's information
                    patronToUpdate.Name = name;
                    patronToUpdate.ContactNumber = contactInformation;

                    dbContext.SaveChanges();

                    Console.WriteLine($"Patron with ID {patronId} has been updated.");
                }
                else
                {
                    Console.WriteLine($"Patron with ID {patronId} was not found in the library.");
                }
            }
        }

        public void ViewAllPatrons()
        {
            using (var dbContext = new LibraryDBContext())
            {
                var patrons = dbContext.Patrons.ToList();

                if (patrons.Count > 0)
                {
                    Console.WriteLine("List of Patrons:");
                    foreach (var patron in patrons)
                    {
                        Console.WriteLine($"ID: {patron.Id}, Name: {patron.Name}, Contact Information: {patron.ContactInformation}");
                    }
                }
                else
                {
                    Console.WriteLine("No patrons found in the library.");
                }
            }
        }
        public void BorrowBook(int patronId, int bookId)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var patron = dbContext.Patrons.FirstOrDefault(p => p.Id == patronId);
                var book = dbContext.Books.FirstOrDefault(b => b.Id == bookId);

                if (patron != null && book != null)
                {
                    if (book.IsAvailable)
                    {
                        // Calculate the return date as 2 weeks from the current date
                        var returnDate = DateTime.Now.AddDays(14);

                        // Create a borrowing record
                        var borrowingRecord = new BorrowingHistory
                        {
                            PatronId = patronId,
                            BookId = bookId,
                            BorrowDate = DateTime.Now,
                            ReturnDate = returnDate
                        };

                        // Update book availability status
                        book.IsAvailable = false;

                        // Add the borrowing record to the database
                        dbContext.BorrowingHistories.Add(borrowingRecord);
                        dbContext.SaveChanges();

                        Console.WriteLine($"Patron {patron.Name} has borrowed the book '{book.Title}'.");
                        Console.WriteLine($"Please return the book by {returnDate.ToString("yyyy-MM-dd")}.");
                    }
                    else
                    {
                        Console.WriteLine($"Book '{book.Title}' is not available for borrowing.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid patron or book ID. Please check and try again.");
                }
            }
        }


        public void ViewBorrowingHistory(int patronId)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var borrowingHistory = dbContext.BorrowingHistories
                    .Include(history => history.patron)
                    .Include(history => history.book)
                    .Where(history => history.PatronId == patronId)
                    .ToList();

                if (borrowingHistory != null && borrowingHistory.Any())
                {
                    var patron = borrowingHistory.First().patron;
                    Console.WriteLine($"Borrowing History for Patron: {patron.Name}");
                    Console.WriteLine("------------------------------------------------");

                    foreach (var history in borrowingHistory)
                    {
                        Console.WriteLine($"Book Title: {history.book.Title}");
                        Console.WriteLine($"Borrow Date: {history.BorrowDate}");
                        Console.WriteLine($"Return Date: {history.ReturnDate ?? DateTime.MinValue}"); // Use DateTime.MinValue if ReturnDate is null
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"No borrowing history found for Patron with ID {patronId}.");
                }
            }
        }



    }
}

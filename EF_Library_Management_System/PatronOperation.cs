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
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                Console.ReadKey();
            }
        }

        public void RemovePatron(int patronId)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var patronToRemove = dbContext.Patrons.FirstOrDefault(patron => patron.Id == patronId);

                if (patronToRemove != null)
                {
                    // Remove the patron from the context and save changes
                    dbContext.Patrons.Remove(patronToRemove);
                    dbContext.SaveChanges();
                    Console.WriteLine($"Patron with ID {patronId} has been removed.");
                }
                else
                {
                    Console.WriteLine($"Patron with ID {patronId} was not found.");
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
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Patron with ID {patronId} was not found in the library.");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();
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
                        Console.WriteLine($"ID: {patron.Id}, Name: {patron.Name}, Contact Information: {patron.ContactNumber}");
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("No patrons found in the library.");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();
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
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Book '{book.Title}' is not available for borrowing.");
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid patron or book ID. Please check and try again.");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();
                }
            }
        }
        public void ReturnBook(int bookId)
        {
            using (var dbContext = new LibraryDBContext())
            {
                var borrowedBook = dbContext.BorrowingHistories
                    .Include(bh => bh.book)
                    .Include(bh => bh.patron)
                    .FirstOrDefault(bh => bh.BookId == bookId && bh.ReturnDate == null);

                if (borrowedBook != null)
                {
                    // Set the return date to the current date (book is returned)
                    borrowedBook.ReturnDate = DateTime.Now;

                    // Update the book's availability status
                    borrowedBook.book.IsAvailable = true;

                    dbContext.SaveChanges();

                    Console.WriteLine($"Book '{borrowedBook.book.Title}' has been returned by Patron '{borrowedBook.patron.Name}'.");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();

                    var returnHistory = new BorrowingHistory
                    {
                        PatronId = borrowedBook.PatronId,
                        BookId = borrowedBook.BookId,
                        BorrowDate = borrowedBook.BorrowDate,
                        ReturnDate = borrowedBook.ReturnDate
                    };

                    dbContext.BorrowingHistories.Add(returnHistory);
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Book not found in the borrowing history or already returned.");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();
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
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"No borrowing history found for Patron with ID {patronId}.");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                    Console.ReadKey();
                }
            }
        }



    }
}

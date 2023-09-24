using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Library_Management_System
{
    internal class Menu
    {
        private BookOperation bookOperation;
        private PatronOperation patronOperation;
        private BorrowingHistory borrowingHistoryOperation;

        public Menu()
        {
            bookOperation = new BookOperation();
            patronOperation = new PatronOperation();
            borrowingHistoryOperation = new BorrowingHistory();
        }

        public void Start()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Book Operations");
                Console.WriteLine("2. Patron Operations");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            BookOperationsMenu();
                            break;

                        case 2:
                            Console.Clear();
                            PatronOperationsMenu();
                            break;

                        case 3:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            }
        }

        private void BookOperationsMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Book Operations");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Remove Book");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. View All Books");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            
                            Console.Write("Enter Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Enter Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Enter Publication Year: ");
                            if (int.TryParse(Console.ReadLine(), out int publicationYear))
                            {
                                bookOperation.AddBook(title, author, publicationYear);
                               
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for publication year.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }

                            break;

                        case 2:
                           
                            Console.WriteLine("List of Books:");
                            bookOperation.ViewAllBooks();
                            Console.Write("Enter the Title of the Book to Remove: ");
                            string bookTitleToRemove = Console.ReadLine();
                            bookOperation.RemoveBook(bookTitleToRemove);
                           
                            break;

                        case 3:
                            
                            Console.WriteLine("List of Books:");
                            bookOperation.ViewAllBooks();
                            Console.Write("Enter the ID of the Book to Update: ");
                            if (int.TryParse(Console.ReadLine(), out int bookIdToUpdate))
                            {
                                Console.Write("Enter new Title: ");
                                string newTitle = Console.ReadLine();
                                Console.Write("Enter new Author: ");
                                string newAuthor = Console.ReadLine();
                                Console.Write("Enter new Publication Year: ");
                                if (int.TryParse(Console.ReadLine(), out int newPublicationYear))
                                {
                                    bookOperation.UpdateBook(bookIdToUpdate, newTitle, newAuthor, newPublicationYear);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input for publication year.");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for book ID.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                           
                            break;

                        case 4:
                            
                            bookOperation.ViewAllBooks();
                           

                            break;

                        case 5:
                            exit = true;
                          
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                Console.WriteLine();
            }
        }

        private void PatronOperationsMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Patron Operations");
                Console.WriteLine("1. Add Patron");
                Console.WriteLine("2. Remove Patron");
                Console.WriteLine("3. Update Patron");
                Console.WriteLine("4. View All Patrons");
                Console.WriteLine("5. Borrow Book");
                Console.WriteLine("6. Return Book");
                Console.WriteLine("7. View Borrowing History");
                Console.WriteLine("8. Return to Main Menu");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                           
                            Console.Write("Enter Patron Name: ");
                            string patronName = Console.ReadLine();
                            Console.Write("Enter Patron Contact Information: ");
                            string contactInformation = Console.ReadLine();
                            patronOperation.AddPatron(patronName, contactInformation);
                            Console.WriteLine("Patron added successfully.");
                            Console.ReadKey(); // Wait for a key press before continuing
                            break;

                        case 2:
                            Console.WriteLine("List of Patrons:");
                            patronOperation.ViewAllPatrons();
                            Console.Write("Enter the ID of the Patron to Remove: ");
                            if (int.TryParse(Console.ReadLine(), out int patronIdToRemove))
                            {
                                patronOperation.RemovePatron(patronIdToRemove);
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for patron ID.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case 3:
                         
                            Console.WriteLine("List of Patrons:");
                            patronOperation.ViewAllPatrons();
                            Console.Write("Enter the ID of the Patron to Update: ");
                            if (int.TryParse(Console.ReadLine(), out int patronIdToUpdate))
                            {
                                Console.Write("Enter new Patron Name: ");
                                string newPatronName = Console.ReadLine();
                                Console.Write("Enter new Contact Number: ");
                                string newContactInformation = Console.ReadLine();
                                patronOperation.UpdatePatron(patronIdToUpdate, newPatronName, newContactInformation);
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for patron ID.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();

                            }
                            break;
                        case 4:
                          
                            Console.WriteLine("List of Patrons:");
                            patronOperation.ViewAllPatrons();
                         
                            break;

                        case 5:
                            
                            Console.WriteLine("List of Patrons:");
                            patronOperation.ViewAllPatrons();
                            Console.Write("Enter the ID of the Patron who wants to borrow a book: ");
                            if (int.TryParse(Console.ReadLine(), out int patronIdToBorrow))
                            {
                                Console.WriteLine("List of Books:");
                                bookOperation.ViewAllBooks();
                                Console.Write("Enter the ID of the Book to borrow: ");
                                if (int.TryParse(Console.ReadLine(), out int bookIdToBorrow))
                                {
                                    patronOperation.BorrowBook(patronIdToBorrow, bookIdToBorrow);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input for book ID.");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for patron ID.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case 6:
                           
                            Console.WriteLine("List of Books:");
                            bookOperation.ViewAllBooks();
                            Console.Write("Enter the ID of the Book to return: ");
                            if (int.TryParse(Console.ReadLine(), out int bookIdToReturn))
                            {
                                patronOperation.ReturnBook(bookIdToReturn);
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for book ID.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case 7:
                            Console.WriteLine("List of Patrons:");
                            patronOperation.ViewAllPatrons();

                            Console.Write("Enter Patron ID to View Borrowing History: ");
                            if (int.TryParse(Console.ReadLine(), out int patronIdToViewHistory))
                            {
                                patronOperation.ViewBorrowingHistory(patronIdToViewHistory);
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for patron ID.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            break;

                        case 8:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ReadKey(); // Wait for a key press before continuing
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.Cinema;
using app.Enums;

namespace app.Services
{
    public class AppBuilder //Service that builds app and all nesssesary objects
    {
        public ShowServise showService = new ShowServise();
        public List<Movie> movies = SerializeService.DeserializeFromFile<Movie>("Movies.json");
        public List<CinemaHall> cinemaHalls = SerializeService.DeserializeFromFile<CinemaHall>("CinemaHalls.json");
        public void Menu() //Shows Main menu
        {
            while (true)
            {
                showService.MenuShow();
                Console.WriteLine();

                bool isValidChoice = false;

                while (!isValidChoice)
                {
                    Console.Write("Your Choice: ");
                    string? input = Console.ReadLine();

                    if (int.TryParse(input, out int choiceValue))
                    {
                        MainMenuChoices choice = (MainMenuChoices)choiceValue;
                        switch (choice)
                        {
                            case MainMenuChoices.Exit:
                                Console.WriteLine("See you next time");
                                return;
                            case MainMenuChoices.BookTicket:
                                showService.BookTicket(showService, cinemaHalls, movies);
                                isValidChoice = true;
                                break;
                            case MainMenuChoices.ShowResevedTickets:
                                showService.TicketShowByName();
                                isValidChoice = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                        break;
                    }
                }
            }
        }
    }

}

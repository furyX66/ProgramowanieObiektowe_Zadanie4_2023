using app.Cinema;
using app.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services
{
    public class ShowServise 
    {
        public void ShowMoviesList(List<Movie> movies) //Shows movie list
        {
            int i = 1;
            foreach (var item in movies)
            {
                Console.WriteLine($"------------------{i++}------------------");
                item.Show();
            }
        }
        public void MenuShow() //Shows main menu
        {
            Console.WriteLine("Welcome to our cinema!");
            Console.WriteLine("Here are all available actions:");
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Book ticket");
            Console.WriteLine("2.Show tickets by name");
        }
        public void BookTicket(ShowServise showServise, List<CinemaHall> cinemaHalls, List<Movie> movies)
        {
            Console.Clear();
            int selectedMovieIndex;
            int counter = 1;
            showServise.ShowMoviesList(movies);
            Console.WriteLine();
            while (true)
            {
                try
                {
                    Console.Write("Plese choose movie you want to see (0-exit): ");
                    selectedMovieIndex = int.Parse(Console.ReadLine());
                    if (selectedMovieIndex == 0)
                    {
                        Console.Clear();
                        return;
                    }
                    Console.Write("Your choice: ");
                    if (selectedMovieIndex >= 1 && selectedMovieIndex <= movies.Count)
                    {
                        var availableMovies = cinemaHalls.Select(hall => hall.Movie).Distinct().ToList();
                        string? selectedMovie = availableMovies[selectedMovieIndex - 1];
                        Console.WriteLine($"Avaliable screenings for '{selectedMovie}':");
                        var screeningTimes = cinemaHalls.Where(hall => hall.Movie == selectedMovie).Select(hall => hall.ScreeningTime).ToList();
                        foreach (var screeningTime in screeningTimes)
                        {
                            Console.WriteLine($"------------------{counter++}------------------");
                            Console.WriteLine(screeningTime.ToString("MM/dd H:mm"));
                        }
                        ScreeningTimeChoice(cinemaHalls, screeningTimes, selectedMovie);
                    }
                    else
                    {
                        Console.WriteLine("Wrong number.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice, try to print number");
                }
            }
        } //Suggests to choose movie
        public void ScreeningTimeChoice(List<CinemaHall> cinemaHalls, List<DateTime> screeningTimes, string selectedMovie)
        {
            while (true)
            {
                try
                {
                    Console.Write("Select a screening by entering its number (0-exit): ");
                    int selectedScreeningIndex = int.Parse(Console.ReadLine());
                    if (selectedScreeningIndex == 0)
                    {
                        return;
                    }
                    else
                    {
                        if (selectedScreeningIndex >= 1 && selectedScreeningIndex - 1 <= screeningTimes.Count)
                        {
                            DateTime selectedScreeningTime = screeningTimes[selectedScreeningIndex - 1];
                            Console.WriteLine($"Selected screening: {selectedScreeningTime.ToString("MM-dd HH:mm")}");
                            var selectedHall = cinemaHalls.FirstOrDefault(hall => hall.Movie == selectedMovie && hall.ScreeningTime == selectedScreeningTime);
                            SeatChoice(selectedHall);
                        }
                        else
                        {
                            Console.WriteLine("No matching hall found.");
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice, try to print number");
                }
            }
        }//Suggests to choose screening time
        public void SeatChoice(CinemaHall selectedHall)
        {
            if (selectedHall != null)
            {
                Dictionary<int, List<int>> seatMap = new Dictionary<int, List<int>>();
                int availableSeatsCount = 0;
                foreach (var seat in selectedHall.SeatList)
                {
                    if (seat.IsAvailable)
                    {
                        if (!seatMap.ContainsKey(seat.Row))
                        {
                            seatMap[seat.Row] = new List<int>();
                        }
                        seatMap[seat.Row].Add(seat.Number);
                        availableSeatsCount++;
                    }
                }
                if (availableSeatsCount == 0)
                {
                    Console.WriteLine("Sorry, there are no available seats for choosing hall.");
                    return; 
                }
                while (true)
                {
                    try
                    {
                        Console.WriteLine($"Available Seats in Hall {selectedHall.HallNumber}:");
                        foreach (var kvp in seatMap)
                        {
                            Console.WriteLine($"Row {kvp.Key}: {string.Join("; ", kvp.Value.Select(seatNum => $"seat {seatNum}"))}");
                        }

                        Console.WriteLine("Enter 0 to exit.");
                        Console.Write("Enter the row number: ");
                        int selectedRow = int.Parse(Console.ReadLine());

                        if (selectedRow == 0)
                        {
                            Console.WriteLine("Exiting seat selection.");
                            break; 
                        }

                        Console.Write("Enter the seat number: ");
                        int selectedSeat = int.Parse(Console.ReadLine());

                        var selectedSeatObj = selectedHall.SeatList.FirstOrDefault(seat => seat.Row == selectedRow && seat.Number == selectedSeat);

                        if (selectedSeatObj != null && selectedSeatObj.IsAvailable)
                        {
                            Console.Write("Enter your name: ");
                            string customerName = Console.ReadLine();

                            Reservation reservation = new Reservation(selectedSeatObj, customerName, selectedHall);
                            reservation.ReserveSeat();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid seat selection or seat already reserved.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid choice, try to print number");
                    }
                }
            }
        }//Suggests to choose Seat
        public void TicketShowByName()
        {
            Console.Clear();
            while (true)
            {
                List<Ticket>? tickets = Reservation.GetTicketsFromFile("Tickets.json");
                Console.Write("Enter the Customer name (0-exit): ");
                string? customerName = Console.ReadLine();
                if (customerName == "0")
                {
                    Console.Clear();
                    return;
                }
                else
                {
                    bool foundTickets = false;

                    foreach (var ticket in tickets)
                    {
                        if (ticket.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Ticket Information:");
                            Console.WriteLine($"Movie: {ticket.Movie}");
                            Console.WriteLine($"Screening Time: {ticket.ScreeningTime.ToString("MM-dd HH:mm")}");
                            Console.WriteLine($"Row: {ticket.Row}");
                            Console.WriteLine($"Number: {ticket.Number}");
                            Console.WriteLine($"Ticket Number: {ticket.TicketNumber}");
                            Console.WriteLine();
                            foundTickets = true;
                        }
                    }

                    if (!foundTickets)
                    {
                        Console.WriteLine("No tickets found for the specified customer name.");
                    }
                }
            }
        } //Shows ticket information
    }
}





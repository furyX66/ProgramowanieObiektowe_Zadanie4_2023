using app.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Cinema
{
    public class Reservation
    {
        #region Ctor
        public Reservation(Seat seat, string customerName, CinemaHall cinemaHall)
        {
            Seat = seat;
            CustomerName = customerName;
            CinemaHall = cinemaHall;
        }
        #endregion
        #region Public members
        public CinemaHall CinemaHall { get; set; }
        public Seat Seat { get; set; }
        public string? CustomerName { get; set; }
        public int TicketNumber { get; set; }
        #endregion
        #region Public methods
        public void ReserveSeat() //Checks json file "CinemaHalls" and updates it when user chooses seat
        {
            if (Seat.IsAvailable)
            {
                Seat.IsAvailable = false;

                List<CinemaHall> cinemaHalls = SerializeService.DeserializeFromFile<CinemaHall>("CinemaHalls.json");
                var hallToUpdate = cinemaHalls.FirstOrDefault(hall => hall.HallNumber == CinemaHall.HallNumber);
                if (hallToUpdate != null)
                {
                    var seatToUpdate = hallToUpdate.SeatList.FirstOrDefault(s => s.Row == Seat.Row && s.Number == Seat.Number);
                    if (seatToUpdate != null)
                    {
                        seatToUpdate.IsAvailable = false;
                    }

                    SerializeService.SerializeToFile("CinemaHalls.json", cinemaHalls);
                    Console.WriteLine($"Seat {Seat.Row}, {Seat.Number} reserved by {CustomerName}");
                }
                else
                {
                    Console.WriteLine("Hall not found.");
                }
            }
            else
            {
                Console.WriteLine($"Seat {Seat.Row}, {Seat.Number} is already reserved.");
            }

            AddToTicketsFile();
        }
        public static List<Ticket>? GetTicketsFromFile(string fileName) // Deserializes ticket from file
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string json = File.ReadAllText(fileName);
                    return JsonConvert.DeserializeObject<List<Ticket>>(json);
                }
                else
                {
                    Console.WriteLine($"File {fileName} not found.");
                    return new List<Ticket>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing tickets from {fileName}: {ex.Message}");
                return new List<Ticket>();
            }
        }
        public void AddToTicketsFile() //Adds reserved seat to json file
        {
            try
            {
                List<Ticket> existingTickets = new List<Ticket>();

                if (File.Exists("Tickets.json"))
                {
                    string existingJson = File.ReadAllText("Tickets.json");
                    if (!string.IsNullOrEmpty(existingJson))
                    {
                        existingTickets = JsonConvert.DeserializeObject<List<Ticket>>(existingJson);
                    }
                }

                Ticket newTicket = new Ticket
                {
                    Movie = CinemaHall.Movie,
                    ScreeningTime = CinemaHall.ScreeningTime,
                    CustomerName = CustomerName,
                    Row = Seat.Row,
                    Number = Seat.Number,
                    TicketNumber = GenerateRandomTicketNumber()
                };

                existingTickets.Add(newTicket);

                string updatedJson = JsonConvert.SerializeObject(existingTickets, Formatting.Indented);
                File.WriteAllText("Tickets.json", updatedJson);

                Console.WriteLine("Reservation added to Tickets.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding reservation to Tickets.json: {ex.Message}");
            }
        }
        private int GenerateRandomTicketNumber() //Generates ticket number
        {
            Random random = new Random();
            return random.Next(1000000, 10000000);
        }
        #endregion
    }
}

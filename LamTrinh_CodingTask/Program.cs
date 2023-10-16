public class HotelReservationSystem
{
    private Dictionary<string, (int sleeps, int numberOfRooms, double price)> roomTypes;

    public HotelReservationSystem()
    {
        // Initialize room types and their respective details
        roomTypes = new Dictionary<string, (int, int, double)>
        {
            { "Single", (1, 2, 30.0) },
            { "Double", (2, 3, 50.0) },
            { "Family", (4, 1, 85.0) }
        };
    }

    public string GetReservationOptions(int numberOfGuests)
    {
        List<string> selectedRooms = new List<string>();
        double totalPrice = 0;

        // Sort room types by price in Descending order
        var sortedRoomTypes = roomTypes.OrderByDescending(rt => rt.Value.price);

        foreach (var roomType in sortedRoomTypes)
        {
            var (sleeps, numberOfRooms, price) = roomType.Value;

            while (numberOfGuests >= sleeps && numberOfRooms > 0)
            {
                selectedRooms.Add(roomType.Key);
                totalPrice += price;
                numberOfGuests -= sleeps;
                numberOfRooms--;
            }

            if (numberOfGuests == 0)
                break;
        }

        if (numberOfGuests > 0)
            return "No option";

        string reservationOptions = string.Join(" ", selectedRooms) + " - $" + totalPrice;
        return reservationOptions;
    }

    public static void Main(string[] args)
    {
        HotelReservationSystem reservationSystem = new HotelReservationSystem();
        while (true)
        {
            Console.WriteLine("Press 'Q' to exit...");
            Console.Write("Enter the number of guests: ");
            string input = Console.ReadLine();
            if (input.ToUpper() == "Q")
                break; // Exit the loop

            if (int.TryParse(input, out int numberOfGuests))
            {
                string reservationOptions = reservationSystem.GetReservationOptions(numberOfGuests);
                Console.WriteLine("Output: " + reservationOptions);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number of guests.");
            }
            Console.WriteLine();
        }
    }
}

using HotelLibrary.Models;
using HotelLibrary.Services;

public static class MainMenu
{
    //Lav selv flere menupunkter til at vælge funktioner for Rooms, bookings m.m.
    //og ligeledes for at kalde async metoder
    public static void showOptions()
    {
        Console.Clear();
        Console.WriteLine("\tVælg et menupunkt\n");
        Console.WriteLine("\t1)\t List hoteller");
        Console.WriteLine("\t1a)\t List hoteller asynkront");
        Console.WriteLine("\t2)\t Opret nyt Hotel ");
        Console.WriteLine("\t2a)\t Opret nyt Hotel asynkront");
        Console.WriteLine("\t3)\t Fjern Hotel");
        Console.WriteLine("\t3a)\t Fjern Hotel asynkront");
        Console.WriteLine("\t4)\t Søg efter hotel udfra hotelnr");
        Console.WriteLine("\t5)\t Opdater et hotel");
        Console.WriteLine("\t6)\t List alle værelser");
        Console.WriteLine("\t7)\t List alle værelser til et bestemt hotel");
        Console.WriteLine("\t8)\t Opret nyt værelse");
        Console.WriteLine("\t9)\t Fjern værelse");
        Console.WriteLine("\t10)\t Søg efter værelse udfra hotelnr og værelsesnr");
        Console.WriteLine("\t11)\t Opdater et værelse");
        Console.WriteLine("\t8)\t Flere menupunkter kommer snart :) ");
        Console.WriteLine("\tQ)\t Afslut");
        Console.Write("\tIndtast valg: ");
    }

    public static bool Menu()
    {
        showOptions();
        switch (Console.ReadLine())
        {
            case "1":
                ShowHotels();
                DoSomething(); //for at demonstrere foreskellen ml synkron og async kald
                Console.ReadKey();
                return true;
            case "1a":
                ShowHotelsAsync();
                DoSomething(); //for at demonstrere foreskellen ml synkron og async kald
                Console.ReadKey();
                return true;
            case "2":
                CreateHotel();
                return true;
            case "2a":
                CreateHotelAsync();
                DoSomething();
                return true;
            case "3":
                DeleteHotel();
                return true;
            case "3a":
                DeleteHotelAsync();
                DoSomething();
                return true;
            case "4":
                GetHotel();
                return true;
            case "5":
                UpdateHotel();
                return true;
            case "6":
                ShowRooms();
                return true;
            case "7":
                ShowRoomsByHotel();
                return true;
            case "8":
                CreateRoom();
                return true;
            case "9":
                DeleteRoom();
                return true;
            case "10":
                SearchRoom();
                return true;
            case "11":
                UpdateRoom();
                return true;
            case "Q":
            case "q": return false;
            default: return true;
        }

    }

    private static void ShowHotels()
    {
        Console.Clear();
        HotelService hs = new HotelService();
        List<Hotel> hotels = hs.GetAllHotel();
        foreach (Hotel hotel in hotels)
        {
            Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
        }
        //Console.ReadKey();
    }
    private static void GetHotel()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer som du ønsker at søge efter :");
        int hotelNo = int.Parse(Console.ReadLine());
        HotelService hs = new HotelService();
        Hotel foundHotel = hs.GetHotelFromId(hotelNo);
        if (foundHotel != null)
        {
            Console.WriteLine($"Hotel fundet {foundHotel.ToString()} ");
        }
        else
        {
            Console.WriteLine("Hotellet findes ikke");
        }
        Console.ReadKey();
    }

    private static void DeleteHotel()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer som du ønsker at fjerne :");
        int hotelNo = int.Parse(Console.ReadLine());
        HotelService hs = new HotelService();
        Hotel hotelToDelete = hs.DeleteHotel(hotelNo);
        if (hotelToDelete != null)
        {
            Console.WriteLine($"Hotel fjernet fra database: {hotelToDelete.ToString()} ");
        }
        else
        {
            Console.WriteLine("Hotellet findes ikke");
        }
        Console.ReadKey();
    }

    private static void CreateHotel()
    {
        //Indlæs data
        Console.Clear();
        Console.WriteLine("Indlæs hotelnr");
        int hotelnr = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Indlæs hotelnavn");
        string navn = Console.ReadLine();
        Console.WriteLine("Indlæs hotel adresse");
        string adresse = Console.ReadLine();

        //Kald hotelservice og vis resultatet
        HotelService hs = new HotelService();
        bool ok = hs.CreateHotel(new Hotel(hotelnr, navn, adresse));
        if (ok)
        {
            Console.WriteLine("Hotellet blev oprettet!");
        }
        else
        {
            Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
        }
        Console.ReadKey();
    }

    private static void UpdateHotel()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer som du ønsker at ændre oplysninger for :");
        int hotelNo = int.Parse(Console.ReadLine());
        
        // Checks if the hotel exists
        HotelService hs = new HotelService();
        Hotel foundHotel = hs.GetHotelFromId(hotelNo);
        if (foundHotel == null)
        {
            Console.WriteLine("Fejl. Hotellet findes ikke");
            Console.ReadKey();
            return;
        }
       
        //Indlæs nye data
        Console.Clear();
        Console.WriteLine("Opdatere hotel : " + foundHotel);
        Console.WriteLine("Indlæs det nye hotelnr");
        string input = Console.ReadLine();
        int hotelnr = String.IsNullOrWhiteSpace(input) ? foundHotel.HotelNr : Convert.ToInt32(input);

        Console.WriteLine("Indlæs det nye hotelnavn");
        input = Console.ReadLine();
        string navn = String.IsNullOrWhiteSpace(input) ? foundHotel.Navn : input;

        Console.WriteLine("Indlæs den nye hotel adresse");
        input = Console.ReadLine();
        string adresse = String.IsNullOrWhiteSpace(input) ? foundHotel.Adresse : input;



        Hotel newHotel = new Hotel(hotelnr, navn, adresse);

        bool ok = hs.UpdateHotel(newHotel, hotelNo);
        if (ok)
        {
            Console.WriteLine("Hotel opdateret");
            Console.WriteLine(newHotel);
        }
        else
        {
            Console.WriteLine("Hotel blev ikke opdateret");
            Console.WriteLine(foundHotel);
        }
        Console.ReadKey();

    }

    private async static Task CreateHotelAsync()
    {
        //Indlæs data
        Console.Clear();
        Console.WriteLine("Indlæs hotelnr");
        int hotelnr = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Indlæs hotelnavn");
        string navn = Console.ReadLine();
        Console.WriteLine("Indlæs hotel adresse");
        string adresse = Console.ReadLine();

        //Kald hotelservice og vis resultatet
        HotelServiceAsync hs = new HotelServiceAsync();
        bool ok = await hs.CreateHotelAsync(new Hotel(hotelnr, navn, adresse));
        if (ok)
        {
            Console.WriteLine("Hotellet blev oprettet!");
        }
        else
        {
            Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
        }
        Console.ReadKey();
    }

    private static async Task DeleteHotelAsync()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer som du ønsker at fjerne :");
        int hotelNo = int.Parse(Console.ReadLine());
        HotelServiceAsync hs = new HotelServiceAsync();
        Hotel hotelToDelete = await hs.DeleteHotelAsync(hotelNo);
        if (hotelToDelete != null)
        {
            Console.WriteLine($"Hotel fjernet fra database: {hotelToDelete.ToString()} ");
        }
        else
        {
            Console.WriteLine("Hotellet findes ikke");
        }
        Console.ReadKey();
    }

    private async static Task ShowHotelsAsync()
    {
        Console.Clear();
        HotelServiceAsync hs = new HotelServiceAsync();
        List<Hotel> hotels = await hs.GetAllHotelAsync();
        foreach (Hotel hotel in hotels)
        {
            Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.HotelNr} Address {hotel.Adresse}");
        }
        //Console.ReadKey();
    }

    private static void ShowRooms()
    {
        Console.Clear();
        RoomService rs = new RoomService();
        List<Room> rooms = rs.GetAllRooms();
        foreach (var room in rooms)
        {
            Console.WriteLine(room);
        }
        Console.ReadKey();
    }

    private static void ShowRoomsByHotel()
    {
        Console.Clear();
        Console.WriteLine("Indlæs hotelnr");
        int hotelNr = Convert.ToInt32(Console.ReadLine());
        RoomService rs = new RoomService();
        List<Room> rooms = rs.GetAllRoomsByHotel(hotelNr);
        foreach (var room in rooms)
        {
            Console.WriteLine(room);
        }
        Console.ReadKey();
    }

    private static void CreateRoom()
    {
        //Indlæs data
        Console.Clear();
        Console.WriteLine("Indlæs hotelnr");
        int hotelNr = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Indlæs værelsesnr");
        int roomNr = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Indlæs type (S, D, F)");
        string type = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(type) || (type[0] != 'S'&& type[0]!= 'D' && type[0] != 'F'))
        {
            //Invalid roomType
            Console.WriteLine("Invalid room type");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Indlæs pris");
        double price= Convert.ToDouble(Console.ReadLine());

        RoomService rs = new RoomService();
        bool ok = rs.CreateRoom(new Room(roomNr, type[0], price, hotelNr));
        if (ok)
        {
            Console.WriteLine("Værelse oprettet");
        }
        else
        {
            Console.WriteLine("Fejl. Værelse ikke oprettet");
        }
        Console.ReadKey();
    }

    private static void SearchRoom()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer for det værelse som du ønsker at finde");
        int hotelNo = int.Parse(Console.ReadLine());
        Console.WriteLine("Indtast værelsesnummer for det værelse som du ønsker at finde");
        int roomNo = int.Parse(Console.ReadLine());

        //Checks if room exists
        RoomService rs = new RoomService();
        Room foundRoom = rs.GetRoomFromId(roomNo, hotelNo);

        if (foundRoom == null)
        {
            Console.WriteLine("Værelse findes ikke");
        }
        else
        {
            Console.WriteLine("Værelse fundet : " + foundRoom);
        }
        Console.ReadKey();
    }

    private static void DeleteRoom()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer for det værelse som du ønsker at fjerne :");
        int hotelNo = int.Parse(Console.ReadLine());
        Console.WriteLine("Indtast værelsesnummer for det værelse som du ønsker at fjerne :");
        int roomNo = int.Parse(Console.ReadLine());
        RoomService rs = new RoomService();
        Room roomToDelete = rs.DeleteRoom(roomNo,hotelNo);
        if (roomToDelete != null)
        {
            Console.WriteLine($"Hotel fjernet fra database: {roomToDelete.ToString()} ");
        }
        else
        {
            Console.WriteLine("Hotellet findes ikke");
        }
        Console.ReadKey();
    }

    private static void UpdateRoom()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer som du ønsker at ændre oplysninger for :");
        int hotelNo = int.Parse(Console.ReadLine());

        Console.WriteLine("Indtast værelses som du ønsker at ændre oplysninger for :");
        int roomNo = int.Parse(Console.ReadLine());

        // Checks if the hotel exists
        RoomService rs = new RoomService();
        Room foundRoom = rs.GetRoomFromId(roomNo, hotelNo);
        if (foundRoom == null)
        {
            Console.WriteLine("Fejl. Hotellet findes ikke");
            Console.ReadKey();
            return;
        }

        //Indlæs nye data
        Console.Clear();
        Console.WriteLine("Opdatere værelse : " + foundRoom);
        Console.WriteLine("Indlæs det nye hotelnr");
        string input = Console.ReadLine();
        int newHotelnr = String.IsNullOrWhiteSpace(input) ? foundRoom.HotelNr : Convert.ToInt32(input);

        Console.WriteLine("Indlæs det nye værelsesnr");
        input = Console.ReadLine();
        int newRoomnr = String.IsNullOrWhiteSpace(input) ? foundRoom.RoomNr : Convert.ToInt32(input);

        Console.WriteLine("Indlæs den nye værelsestype (S,D,F)");
        string type = Console.ReadLine();
        char typeChar;
        if (String.IsNullOrEmpty(type))
        {
            typeChar = foundRoom.Types;
        }
        else
        {
            typeChar = type[0];
        }
        if (type[0] != 'S' || type[0] != 'D' || type[0] != 'F')
        {
            //Invalid roomType
            Console.WriteLine("Invalid room type");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Indlæs den nye pris");
        input = Console.ReadLine();
        double newPrice = String.IsNullOrWhiteSpace(input) ? foundRoom.Pris : Convert.ToDouble(input);

        Room newRoom = new Room(newRoomnr, typeChar, newPrice, newHotelnr);

        bool ok = rs.UpdateRoom(newRoom, roomNo, hotelNo);
        if (ok)
        {
            Console.WriteLine("Værelse opdateret");
            Console.WriteLine(newRoom);
        }
        else
        {
            Console.WriteLine("Værelse blev ikke opdateret");
            Console.WriteLine(foundRoom);
        }
        Console.ReadKey();

    }

    private static void DoSomething()
    {
        for (int i = 0; i < 30; i++)
        {
            Thread.Sleep(100);
            Console.WriteLine(i + " i GUI i main thread");
        }
        Console.ReadLine();
    }
}
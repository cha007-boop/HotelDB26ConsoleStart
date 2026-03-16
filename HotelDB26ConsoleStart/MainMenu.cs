// See https://aka.ms/new-console-template for more information
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
                //DoSomething(); for at demonstrere foreskellen ml synkron og async kald
                return true;
            case "1a":
                ShowHotelsAsync();
                //DoSomething(); for at demonstrere foreskellen ml synkron og async kald
                return true;
            case "2":
                CreateHotel();
                return true;
            case "4":
                GetHotel();
                return true;
            case "Q":
            case "q": return false;
            default: return true;
        }

    }

    private static void ShowHotels()
    {
        Console.Clear();
        //HotelService hs = new HotelService();
        //List<Hotel> hotels = hs.GetAllHotel();
        //foreach (Hotel hotel in hotels)
        //{
        //    Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
        //}
        Console.ReadKey();
    }
    private static void GetHotel()
    {
        Console.Clear();
        Console.WriteLine("Indtast hotel nummer som du ønsker at søge efter :");
        //int hotelNo = int.Parse(Console.ReadLine());
        //HotelService hs = new HotelService();
        //Hotel foundHotel = hs.GetHotelFromId(hotelNo);
        //if (foundHotel != null)
        //{
        //    Console.WriteLine($"Hotel fundet {foundHotel.ToString()} ");
        //}
        //else
        //{
        //    Console.WriteLine("Hotellet findes ikke");
        //}
        Console.ReadKey();
    }



    private static void CreateHotel()
    {
        //Indlæs data
        Console.Clear();
        //Console.WriteLine("Indlæs hotelnr");
        //int hotelnr = Convert.ToInt32(Console.ReadLine());
        //Console.WriteLine("Indlæs hotelnavn");
        //string navn = Console.ReadLine();
        //Console.WriteLine("Indlæs hotel adresse");
        //string adresse = Console.ReadLine();

        ////Kald hotelservice og vis resultatet
        //HotelService hs = new HotelService();
        //bool ok = hs.CreateHotel(new Hotel(hotelnr, navn, adresse));
        //if (ok)
        //{
        //    Console.WriteLine("Hotellet blev oprettet!");
        //}
        //else
        //{
        //    Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
        //}
        Console.ReadKey();
    }



    private async static Task ShowHotelsAsync()
    {
        Console.Clear();
        //HotelServiceAsync hs = new HotelServiceAsync();
        //List<Hotel> hotels = await hs.GetAllHotelAsync();
        //foreach (Hotel hotel in hotels)
        //{
        //    Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.HotelNr} Address {hotel.Adresse}");
        //}
        Console.ReadKey();
    }

    private static void DoSomething()
    {
        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(100);
            Console.WriteLine(i + " i GUI i main thread");
        }
        Console.ReadLine();
    }
}
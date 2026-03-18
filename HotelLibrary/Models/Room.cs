using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Models
{
    public class Room
    {
        public int RoomNr { get; set; }
        public char Types { get; set; }
        public double Pris { get; set; }
        public int HotelNr { get; set; }

        public Room()
        {

        }
        public Room(int nr, char types, double pris)
        {
            RoomNr = nr;
            Types = types;
            Pris = pris;
        }

        public Room(int nr, char types, double pris, int hotelNr) : this(nr, types, pris)
        {
            HotelNr = hotelNr;
        }

        public override string ToString()
        {
            return $"Room = {RoomNr},{(HotelNr != 0 ? $" Hotel = {HotelNr}," : "")} Types = {Types}, Pris = {Pris}";
        }
    }
}

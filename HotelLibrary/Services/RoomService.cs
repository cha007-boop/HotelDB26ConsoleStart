using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Services
{
    public class RoomService : Connection, IRoomService
    {
        private string _queryString = "SELECT Room_No, Hotel_No, Types, Price FROM Room";
        private string _queryByHotel = "select Room_No, Hotel_No, Types, Price from room where Hotel_No=@HotelNo";
        private string _querySearchRoomHotel = "Select * FROM Room where Room_No=@RoomNo AND Hotel_No=@HotelNo";
        private string _insertSql = "INSERT INTO Room Values(@RoomNo, @HotelNo, @Type, @Price)";
        private string _deleteSql = "Delete from Room Where Room_No = @RoomNo AND Hotel_No = @HotelNo";
        private string _updateSql = "Update Room SET Room_No=@NewRoomNo, Hotel_No=@NewHotelNo, Types=@NewType, Price=@NewPrice WHERE Room_No=@RoomNoToUpdate AND Hotel_No=@HotelNoToUpdate";

        public bool CreateRoom(Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_insertSql, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@RoomNo", room.RoomNr);
                    command.Parameters.AddWithValue("@HotelNo", room.HotelNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return false;
            }
        }

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Room roomToDelete = GetRoomFromId(roomNr, hotelNr);
                    SqlCommand command = new SqlCommand(_deleteSql, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@RoomNo", roomNr);
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    int noOfRows = command.ExecuteNonQuery();

                    if (noOfRows == 1)
                    {
                        return roomToDelete;
                    }

                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return null;
            }
        }

        public List<Room> GetAllRoomsByHotel(int hotelNrToSearch)
        {
            //return GetAllRooms().Where(r => r.HotelNr == hotelNr).ToList();
            List<Room> foundRooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_queryByHotel, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@HotelNo", hotelNrToSearch);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int roomNr = reader.GetInt32("Room_No");
                        int hotelNr = reader.GetInt32("Hotel_No");
                        char type = reader.GetString("Types")[0];
                        double price = reader.GetDouble("Price");
                        Room room = new Room(roomNr, type, price, hotelNr);
                        foundRooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return foundRooms;
        }
       
        public List<Room> GetAllRooms()
        {
            List<Room> foundRooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_queryString, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int roomNr = reader.GetInt32("Room_No");
                        int hotelNr = reader.GetInt32("Hotel_No");
                        char type = reader.GetString("Types")[0];
                        double price = reader.GetDouble("Price");
                        Room room = new Room(roomNr, type, price, hotelNr);
                        foundRooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return foundRooms;
        }

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_querySearchRoomHotel, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@RoomNo", roomNr);
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    int foundHotelNr = reader.GetInt32("Hotel_No");
                    int foundRoomNr = reader.GetInt32("Room_No");
                    char type = reader.GetString("Types")[0];
                    double price = reader.GetDouble("Price");
                    Room room = new Room(foundRoomNr, type, price, foundHotelNr);
                    reader.Close();
                    return room;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return null;
            }
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {   
                    SqlCommand command = new SqlCommand(_updateSql, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@RoomNoToUpdate", roomNr);
                    command.Parameters.AddWithValue("@HotelNoToUpdate", hotelNr);
                    command.Parameters.AddWithValue("@NewRoomNo", room.RoomNr);
                    command.Parameters.AddWithValue("@NewHotelNo", room.HotelNr);
                    command.Parameters.AddWithValue("@NewType", room.Types);
                    command.Parameters.AddWithValue("@NewPrice", room.Pris);
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return false;
            }
        }
    }
}

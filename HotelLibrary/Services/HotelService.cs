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
    public class HotelService : Connection, IHotelService
    {
        private string _queryString = "SELECT Hotel_No, Name, Address FROM Hotel";
        private string _insertSql = "Insert INTO Hotel Values(@ID, @Navn, @Adresse)";
        private string _querySearchId = "SELECT Hotel_No, Name, Address From Hotel Where Hotel_No=@ID";
        private string _deleteSql = "Delete from Hotel Where Hotel_No=@ID";
        private string _updateSql = "Update Hotel SET Hotel_No=@NewID, Name=@NewName, Address=@NewAddress Where Hotel_No=@IDToUpdate";

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_insertSql, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
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

        public Hotel DeleteHotel(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Hotel hotelToDelete = GetHotelFromId(hotelNr);
                    SqlCommand command = new SqlCommand(_deleteSql, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    int noOfRows = command.ExecuteNonQuery();

                    if (noOfRows == 1)
                    {
                        return hotelToDelete;
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

        public List<Hotel> GetAllHotel()
        {
            List<Hotel> foundHotels = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_queryString, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        foundHotels.Add(hotel);
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
            return foundHotels;

        }

        public Hotel GetHotelFromId(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_querySearchId, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    int foundHotelNr = reader.GetInt32("Hotel_No");
                    string hotelNavn = reader.GetString("Name");
                    string hotelAdr = reader.GetString("Address");
                    Hotel hotel = new Hotel(foundHotelNr, hotelNavn, hotelAdr);
                    reader.Close();
                    return hotel;
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

        public List<Hotel> GetHotelsByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateSql, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@IDToUpdate", hotelNr);
                    command.Parameters.AddWithValue("@NewID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@NewName", hotel.Navn);
                    command.Parameters.AddWithValue("@NewAddress", hotel.Adresse);
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

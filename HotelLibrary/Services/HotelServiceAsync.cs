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
    public class HotelServiceAsync : Connection, IHotelServiceAsync
    {
        private string _queryString = "SELECT Hotel_No, Name, Address FROM Hotel";
        private string _queryHotelNo = "Select * FROM Hotel where Hotel_No = HotelNo";
        private string _insertSql = "Insert INTO Hotel Values(@ID, @Name, @Address)";
        private string _deleteSql = "Delete from Hotel Where Hotel_No=@HotelNo";
        private string _updateSql = "Update Hotel SET Hotel_No=@NewID, Name=@NewName, Address=@NewAddress Where Hotel_No=@IDToUpdate";

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_insertSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Name", hotel.Navn);
                    command.Parameters.AddWithValue("@Address", hotel.Adresse);
                    Thread.Sleep(100);
                    int noOfRows = await command.ExecuteNonQueryAsync();
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

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Hotel hotelToDelete = await GetHotelFromIdAsync(hotelNr);
                    SqlCommand command = new SqlCommand(_deleteSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    Thread.Sleep(100);
                    int noOfRows = await command.ExecuteNonQueryAsync();

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

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> foundHotels = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(100);
                    while (await reader.ReadAsync())
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

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_queryHotelNo, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(100);
                    await reader.ReadAsync();
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

        public Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@IDToUpdate", hotelNr);
                    command.Parameters.AddWithValue("@NewID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@NewName", hotel.Navn);
                    command.Parameters.AddWithValue("@NewAddress", hotel.Adresse);
                    Thread.Sleep(100);
                    int noOfRows = await command.ExecuteNonQueryAsync();
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

using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorHotelDB26.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        private IRoomService _roomService;
        public List<Room> MyRooms { get; set; }
        public int TheHotelNo { get; set; }

        public GetAllRoomsModel(IRoomService roomService)
        {
            _roomService = roomService;
        }
        public IActionResult OnGet(int cid)
        {
            try
            {
                MyRooms = _roomService.GetAllRoomsByHotel(cid);
                TheHotelNo = cid;
            }
            catch (Exception ex)
            {
                MyRooms = new List<Room>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}

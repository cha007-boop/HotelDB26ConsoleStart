using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorHotelDB26.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private IRoomService _roomService;
        [BindProperty]
        public Room NewRoom { get; set; }

        public CreateModel(IRoomService roomService)
        {
            _roomService = roomService;
        }
        public void OnGet(int hotelnr)
        {
            NewRoom = new Room();
            NewRoom.HotelNr = hotelnr;
        }

        public IActionResult OnPost()
        {            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _roomService.CreateRoom(NewRoom);
            return RedirectToPage("/Hotels/GetAllHotels");
        }
    }
}

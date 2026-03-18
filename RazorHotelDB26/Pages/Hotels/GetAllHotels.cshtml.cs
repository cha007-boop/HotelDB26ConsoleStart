using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorHotelDB26.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        private IHotelServiceAsync _hotelServiceAsync;

        public List<Hotel> Hotels { get; set; }

        public GetAllHotelsModel(IHotelServiceAsync hotelServiceAsync)
        {
            _hotelServiceAsync = hotelServiceAsync;
        }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                Hotels = await _hotelServiceAsync.GetAllHotelAsync();
            }
            catch (Exception ex)
            {
                Hotels = new List<Hotel>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageAccommodation.Repository
{
    public class Methods
    {
        public List<SelectListItem> Status { get; set; }

        public Methods()
        {
            Status = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Paid", Value="Paid"},
                new SelectListItem() { Text="Unpaid", Value="Unpaid"},
            };
        }
    }
}

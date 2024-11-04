using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerUI.Data;
using Shared.ApiModels;

namespace CustomerUI.Pages
{
    public class CreateCustomer(CustomerService service) : PageModel
    {
        [BindProperty]
        public CustomerDto Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await service.AddCustomerAsync(Customer);

            return RedirectToPage("/Index");
        }
    }
}
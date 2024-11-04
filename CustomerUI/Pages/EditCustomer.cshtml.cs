using CustomerUI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.ApiModels;

namespace CustomerUI.Pages;

public class EditCustomer(CustomerService service) : PageModel
{
    [BindProperty] 
    public CustomerDto? Customer { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Customer = await service.GetCustomerAsync(id);

        if (Customer == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        Customer.Id = id;
        await service.UpdateCustomerAsync(id, Customer);
        return RedirectToPage("/Index");
    }
}
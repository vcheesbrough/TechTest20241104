using CustomerUI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.ApiModels;

namespace CustomerUI.Pages;

public class IndexModel(CustomerService service) : PageModel
{
    public List<CustomerDto> Customers { get; set; }

    public async Task OnGetAsync()
    {
        Customers = (await service.GetCustomersAsync()).ToList();
    }

    public async Task<IActionResult> OnGetDeleteCustomerAsync(int id)
    {
        await service.DeleteCustomerAsync(id);
        return RedirectToPage("/Index");
    }
}
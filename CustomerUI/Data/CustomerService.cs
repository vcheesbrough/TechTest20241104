using Shared.ApiModels;

     namespace CustomerUI.Data
     {
         public class CustomerService(HttpClient httpClient)
         {
             public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
             {
                 return await httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/Customers");
             }

             public async Task<CustomerDto> GetCustomerAsync(int id)
             {
                 return await httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{id}");
             }

             public async Task AddCustomerAsync(CustomerDto customer)
             {
                 var httpResponse = await httpClient.PostAsJsonAsync("api/customers", customer);
                 httpResponse.EnsureSuccessStatusCode();
             }

             public async Task UpdateCustomerAsync(int id, CustomerDto customer)
             {
                 var responseMessage = await httpClient.PutAsJsonAsync($"api/customers/{id}", customer);
                 responseMessage.EnsureSuccessStatusCode();
             }

             public async Task DeleteCustomerAsync(int id)
             {
                 var httpResponse = await httpClient.DeleteAsync($"api/customers/{id}");
                 httpResponse.EnsureSuccessStatusCode();
             }
         }
     }
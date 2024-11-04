using AutoMapper;
using CustomerApi.Data.Models;
using Shared.ApiModels;

namespace CustomerApi.Automapper;

public class CustomerDaoToCustomerDtoProfile : Profile
{
    public CustomerDaoToCustomerDtoProfile()
    {
        CreateMap<CustomerDao, CustomerDto>()
            .ReverseMap();
    }
}
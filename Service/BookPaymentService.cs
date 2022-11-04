using DependencyRoomBooking.Controllers;
using DependencyStore.Service.Contracts;
using RestSharp;

namespace DependencyStore.Service;

public class BookPaymentService : IBookPaymentService
{
    public async Task<PaymentResponse?> ObterPagamentoDoLivro(BookRoomCommand command){
       
        var client = new RestClient("https://payments.com");
        var request = new RestRequest()
            .AddQueryParameter("api_key", "c20c8acb-bd76-4597-ac89-10fd955ac60d")
            .AddJsonBody(new
            {
                User = command.Email,
                CreditCard = command.CreditCard
            });
        var response = await client.PostAsync<PaymentResponse>(request);
        
        return response;
    }
}
using DependencyRoomBooking.Controllers;

namespace DependencyStore.Service.Contracts;

public interface IBookPaymentService{
    Task<PaymentResponse?> ObterPagamentoDoLivro(BookRoomCommand command);
}
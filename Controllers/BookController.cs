using DependencyStore.Repositories.Contracts;
using DependencyStore.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DependencyRoomBooking.Controllers;

[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookPaymentService _bookPaymentService;

    public BookController(IBookRepository bookRepository, 
                          IBookPaymentService bookPaymentService){
        _bookRepository = bookRepository;
        _bookPaymentService = bookPaymentService;
    }


    public async Task<IActionResult> Book(BookRoomCommand command)
    {
        var usuario = await _bookRepository.ObterUsuario(command);

        if (usuario == null)
            return NotFound();

        var room = await _bookRepository.ObterSalaDisponivel(command);

        if (room is not null)
            return BadRequest();

        var response =  await _bookPaymentService.ObterPagamentoDoLivro(command);

        if(!PagamentoConcluido(response)) return BadRequest();

        var reserva = CriarReserva(command);

        await _bookRepository.SalvarDados(reserva);

        return Ok();
    }

    private Book CriarReserva(BookRoomCommand command)
    {
       return  new Book(command.Email,
                        command.RoomId,
                        command.Day);
    }

    private bool PagamentoConcluido(PaymentResponse? response)
    {
         // Se a requisição não pode ser completa
         if (response is null)
            return false;

        // Se o status foi diferente de "pago"
        if (response?.Status != "paid") 
            return false;

        return true;
    }
}

public class BookRoomCommand
{
    public string? Email { get; set; }
    public Guid RoomId { get; set; }
    public DateTime Day { get; set; }
    public CreditCard? CreditCard { get; set; }
}

public record PaymentResponse(int Code, string Status);

public record Customer(string Email);

public record Room(Guid Id, string Name);

public record Book(string? Email, Guid Room, DateTime Date);

public record CreditCard(string Number, string Holder, string Expiration, string Cvv);
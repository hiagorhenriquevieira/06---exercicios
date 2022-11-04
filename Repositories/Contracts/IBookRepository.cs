using DependencyRoomBooking.Controllers;

namespace DependencyStore.Repositories.Contracts;

public interface IBookRepository 
{
    Task<Book?> ObterUsuario(BookRoomCommand command);

    Task<Book?> ObterSalaDisponivel(BookRoomCommand command);
    Task SalvarDados(Book book);
}


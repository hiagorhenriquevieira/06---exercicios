using Dapper;
using DependencyRoomBooking.Controllers;
using DependencyStore.Repositories.Contracts;
using Microsoft.Data.SqlClient;

namespace DependencyStore.Repositories;

public class BookRepository : IBookRepository
{
    private readonly SqlConnection _connection;

    public BookRepository(SqlConnection connection)
    => _connection = connection;

    public async Task<Book?> ObterUsuario(BookRoomCommand command)
    {
        return await _connection
             .QueryFirstOrDefaultAsync<Book?>("SELECT * FROM [Customer] WHERE [Email]=@email",
                new { command.Email });
    } 
     public async Task<Book?> ObterSalaDisponivel(BookRoomCommand command)
    {
       return await _connection.QueryFirstOrDefaultAsync<Book?>(
            "SELECT * FROM [Book] WHERE [Room]=@room AND [Date] BETWEEN @dateStart AND @dateEnd",
            new
            {
                Room = command.RoomId,
                DateStart = command.Day.Date,
                DateEnd = command.Day.Date.AddDays(1).AddTicks(-1),
            });
    }

    public async Task SalvarDados(Book book)
    {
        await _connection.ExecuteAsync("INSERT INTO [Book] VALUES(@date, @email, @room)", new
        {
            book.Date,
            book.Email,
            book.Room
        });
    }
}
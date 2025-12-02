using Microsoft.AspNetCore.Mvc;

public class ReserveService : IReserveService
{
    private readonly IReserveRepository _reserveRepository;

    public ReserveService(IReserveRepository reserveRepository)
    {
        _reserveRepository = reserveRepository;
    }

    public IActionResult CreateReserve(ReserveDto reserveDto)
    {
        //checar se há reservas conflitantes
        bool conflict = _reserveRepository.ValidateReserve(reserveDto);
        if (conflict)
        {
            return new ObjectResult("Já há uma reserva nesse horário.") { StatusCode = 409 };    
        }

        //criar reserva
        string newReserveId = _reserveRepository.CreateReserve(reserveDto);

        return new ObjectResult(newReserveId) { StatusCode = 200 };
    }

    public DynamicResponse UpdateReserve(ReserveDto reserveDto)
    {
        Reserve updatedReserve = _reserveRepository.UpdateReserve(reserveDto);
        return new DynamicResponse
        {
            Message = "Reserva atualizada com sucesso.",
            StatusCode = 200,
            Data = new List<dynamic> { updatedReserve }
        };
    }

    public IActionResult DeleteReserve(string id)
    {
        _reserveRepository.DeleteReserve(id);
        return new ObjectResult("Sala deletada com sucesso.") { StatusCode = 200 };
    }
    
    
    public DynamicResponse GetAllReservesByUserId(string userId)
    {
        var reserves = _reserveRepository.GetAllReservesByUserId(userId);
        return new DynamicResponse
        {
            Message = "Reservas retornadas com sucesso.",
            StatusCode = 200,
            Data = reserves.Cast<dynamic>().ToList()
        };
    }
}
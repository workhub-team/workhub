using Microsoft.AspNetCore.Mvc;
using workhub_api.Migrations;

public class ReserveService : IReserveService
{
    private readonly IReserveRepository _reserveRepository;
    private readonly IRoomRepository _roomRepository;

    public ReserveService(IReserveRepository reserveRepository, IRoomRepository roomRepository)
    {
        _reserveRepository = reserveRepository;
        _roomRepository = roomRepository;
    }

    public IActionResult CreateReserve(ReserveDto reserveDto)
    {
        //checar se há reservas conflitantes
        bool conflict = _reserveRepository.ValidateReserve(reserveDto);
        if (!conflict)
        {
            return new ObjectResult("Já há uma reserva nesse horário.") { StatusCode = 409 };    
        }

        //criar reserva
        string newReserveId = _reserveRepository.CreateReserve(reserveDto);

        return new ObjectResult(newReserveId) { StatusCode = 200 };
    }

    public DynamicResponse VerifyReserve(ReserveDto reserveDto)
    {
        bool isAvailable = _reserveRepository.ValidateReserve(reserveDto);
        if (!isAvailable)
        {
            return new DynamicResponse
            {
                Message = "Já há uma reserva nesse horário.",
                StatusCode = 409,
            };
        }

        Room room = _roomRepository.GetRoomById(reserveDto.RoomId);
        var roomSize = room.Seats;
        var roomIsShared = room.IsShared;
        decimal price = GetRoomPrice(roomSize, roomIsShared, reserveDto.ReservedPeriod);

        return new DynamicResponse
        {
            Message = "Horário disponível para reserva.",
            StatusCode = 200,
            Data = new List<dynamic>
            {
                new VerifyResponseDto
                {
                    IsAvailable = true,
                    Price = price,
                    RequestedDate = reserveDto.ReservedDay,
                    RequestedPeriod = reserveDto.ReservedPeriod
                }
            }
        };
    }

    private decimal GetRoomPrice(int roomSize, bool roomIsShared, string reservedPeriod)
    {
        if (roomIsShared) {
            return 80.00m;
        }
        else
        {
            bool isHalfaDay = false;
            if (reservedPeriod == "manhã" || reservedPeriod == "tarde") {
                isHalfaDay = true;
            }

            if (roomSize == 4) {
                if (isHalfaDay) return 180.00m;
                else return 300.00m;    
            }
            else if (roomSize == 5) {
                if (isHalfaDay) return 220.00m;
                else return 380.00m;  
            }
            else if (roomSize == 10) {
                if (isHalfaDay) return 350.00m;
                else return 600.00m;  
            }
            return 0.00m;
        }
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
        // List<UserReservesDto> reserveResponse = new List<UserReservesDto>();

        // foreach (var reserve in reserves)
        // {
        //     reserveResponse.Add(new UserReservesDto
        //     {
        //         ReserveDate = reserve.ReservedDay,
        //         ReservePeriod = reserve.ReservedPeriod,
        //         RoomName = _roomRepository.GetRoomById(reserve.RoomId).Name,
        //         AccessCode = reserve.AccessCode
        //     });
        // }

        return new DynamicResponse
        {
            Message = "Reservas retornadas com sucesso.",
            StatusCode = 200,
            Data = reserves.Cast<dynamic>().ToList()
        };
    }
}
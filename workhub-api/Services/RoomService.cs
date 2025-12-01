using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class RoomService : IRoomService
{

    private readonly IRoomRepository _roomRepository;
    private readonly WorkHubContext _context;

    public RoomService(IRoomRepository roomRepository, WorkHubContext context)
    {
        _context = context;
        _roomRepository = roomRepository;
    }


    public IActionResult CreateRoom(RoomDto roomDto)
    {
        //primeiro checo se o email é válido
        //depois, se o email já ta sendo usado uwu
        Room foundRoom = _roomRepository.GetRoomByName(roomDto.Name);

        //se tiver, acuso o golpe
        if (foundRoom != null)
        {
            return new ObjectResult("Sala com nome já registrado.") { StatusCode = 409 };    
        }
        
        //se n tiver, registro o novo carinha
        string newRoomId = _roomRepository.CreateRoom(roomDto);

        return new ObjectResult(newRoomId) { StatusCode = 200 };
        
    }

    public DynamicResponse UpdateRoom(RoomDto roomDto)
    {
        Room updatedRoom = _roomRepository.UpdateRoom(roomDto);
        // return new ObjectResult(unityId) { StatusCode = 200 };
        return new DynamicResponse
        {
            Message = "Sala criada com sucesso.",
            StatusCode = 200,
            Data = new List<dynamic> { updatedRoom }
        };
    }

    public IActionResult DeleteRoom(string id)
    {
        _roomRepository.DeleteRoom(id);
        return new ObjectResult("Sala deletada com sucesso.") { StatusCode = 200 };
    }
    public DynamicResponse GetAllRoomsByUnityId(string unityId)
    {
        List<Unity> unities = _roomRepository.GetAllRooms();
        return new DynamicResponse
        {
            Message = "Salas retornadas com sucesso.",
            StatusCode = 200,
            Data = unities.Cast<dynamic>().ToList()
        };
    }
    

}
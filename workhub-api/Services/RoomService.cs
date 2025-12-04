using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnityRepository _unityRepository;

    public RoomService(IRoomRepository roomRepository, IUnityRepository unityRepository)
    {
        _roomRepository = roomRepository;
        _unityRepository = unityRepository;
    }

    public IActionResult CreateRoom(RoomDto roomDto)
    {
        //checar se a unidade existe
        Unity foundUnity = _unityRepository.GetUnityById(roomDto.UnityId);
        if (foundUnity == null)
        {
            return new ObjectResult("Unidade não encontrada.") { StatusCode = 404 };    
        }

        //checar se a sala já existe
        Room foundRoom = _roomRepository.GetRoomByName(roomDto.Name);
        if (foundRoom != null && foundRoom.UnityId == roomDto.UnityId)
        {
            return new ObjectResult("Sala com nome já registrado.") { StatusCode = 409 };    
        }
        
        //se tiver tudo certo, registro o novo carinha
        string newRoomId = _roomRepository.CreateRoom(roomDto);

        return new ObjectResult(newRoomId) { StatusCode = 200 };
    }

    public DynamicResponse UpdateRoom(RoomDto roomDto)
    {
        Room updatedRoom = _roomRepository.UpdateRoom(roomDto);
        // return new ObjectResult(unityId) { StatusCode = 200 };
        return new DynamicResponse
        {
            Message = "Sala atualizada com sucesso.",
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
        List<Room> unities = _roomRepository.GetAllRoomsByUnityId(unityId);
        return new DynamicResponse
        {
            Message = "Salas retornadas com sucesso.",
            StatusCode = 200,
            Data = unities.Cast<dynamic>().ToList()
        };
    }
}
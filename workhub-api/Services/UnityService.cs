using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class UnityService : IUnityService
{

    private readonly IUnityRepository _unityRepository;
    private readonly IRoomRepository _roomRepository;

    public UnityService(IUnityRepository unityRepository, IRoomRepository roomRepository)
    {
        _unityRepository = unityRepository;
        _roomRepository = roomRepository;
    }


    public IActionResult CreateUnity(UnityDto unityDto)
    {
        //primeiro checo se o email é válido
        //depois, se o email já ta sendo usado uwu
        Unity foundUnity = _unityRepository.GetUnityByName(unityDto.Name);

        //se tiver, acuso o golpe
        if (foundUnity != null)
        {
            return new ObjectResult("Unidade com nome já registrado.") { StatusCode = 409 };    
        }
        
        //se n tiver, registro o novo carinha
        string newUserId = _unityRepository.CreateUnity(unityDto);

        return new ObjectResult(newUserId) { StatusCode = 200 };
        
    }

    public DynamicResponse UpdateUnity(UnityDto unityDto)
    {
        Unity updatedUnity = _unityRepository.UpdateUnity(unityDto);
        // return new ObjectResult(unityId) { StatusCode = 200 };
        return new DynamicResponse
        {
            Message = "Unidade atualizada com sucesso.",
            StatusCode = 200,
            Data = new List<dynamic> { updatedUnity }
        };
    }

    public IActionResult DeleteUnity(string id)
    {
        _unityRepository.DeleteUnity(id);
        return new ObjectResult("Unidade deletada com sucesso.") { StatusCode = 200 };
    }
    public DynamicResponse GetAllUnities()
    {
        List<Unity> unities = _unityRepository.GetAllUnities();
        return new DynamicResponse
        {
            Message = "Unidades retornadas com sucesso.",
            StatusCode = 200,
            Data = unities.Cast<dynamic>().ToList()
        };
    }
    
    public DynamicResponse GetAllUnitiesWithRooms()
    {
        List<Unity> unities = _unityRepository.GetAllUnities();

        List<UnityWithRoomsDto> unitiesWithRooms = new List<UnityWithRoomsDto>();

        foreach (var unity in unities)
        {
            unitiesWithRooms.Add(new UnityWithRoomsDto
            {
                Id = unity.Id,
                Name = unity.Name,
                Address = unity.Address,
                Rooms = _roomRepository.GetAllRoomsByUnityId(unity.Id)
            });
        }

        return new DynamicResponse
        {
            Message = "Unidades retornadas com sucesso.",
            StatusCode = 200,
            Data = unitiesWithRooms.Cast<dynamic>().ToList()
        };
    }
}
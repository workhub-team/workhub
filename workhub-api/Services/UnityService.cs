using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class UnityService : IUnityService
{

    private readonly IUnityRepository _unityRepository;

    public UnityService(IUnityRepository unityRepository)
    {
        _unityRepository = unityRepository;
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
            Message = "Unidade criada com sucesso.",
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
    

}
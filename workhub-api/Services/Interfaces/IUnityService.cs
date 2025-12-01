using Microsoft.AspNetCore.Mvc;

public interface IUnityService
{
    public DynamicResponse GetAllUnities();
    public IActionResult CreateUnity(UnityDto unityDto);
    public DynamicResponse UpdateUnity(UnityDto unityDto);
    public IActionResult DeleteUnity(string id);
}
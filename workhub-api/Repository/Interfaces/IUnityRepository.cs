public interface IUnityRepository
{
    Unity GetUnityById(string id);
    Unity GetUnityByName(string name);
    List<Unity> GetAllUnities();
    string CreateUnity(UnityDto unityDto);
    Unity UpdateUnity(UnityDto unityDto);
    void DeleteUnity(string id);
}
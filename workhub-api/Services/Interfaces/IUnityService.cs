public interface IUnityService
{
    public Unity GetUnityById(string id);
    public List<Unity> GetAllUnities();
    public Unity CreateUnity(Unity unity);
    public Unity UpdateUnity(Unity unity);
    public void DeleteUnity(string id);
}
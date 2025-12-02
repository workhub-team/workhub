public interface IReserveRepository
{
    Reserve GetReserveById(string id);
    List<Reserve> GetAllReservesByUserId(string userId);
    string CreateReserve(ReserveDto reserveDto);
    Reserve UpdateReserve(ReserveDto reserveDto);
    void DeleteReserve(string id);
    bool ValidateReserve(ReserveDto reserveDto);
}
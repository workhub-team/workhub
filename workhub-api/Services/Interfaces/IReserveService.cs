using Microsoft.AspNetCore.Mvc;

public interface IReserveService
{
    public DynamicResponse GetAllReservesByUserId(string userId);
    public IActionResult CreateReserve(ReserveDto reserveDto);
    public DynamicResponse UpdateReserve(ReserveDto reserveDto);
    public IActionResult DeleteReserve(string id);
}
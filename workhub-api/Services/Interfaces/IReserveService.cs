using Microsoft.AspNetCore.Mvc;

public interface IReserveService
{
    public DynamicResponse GetAllReservesByUserId(string userId);
    public DynamicResponse VerifyReserve(ReserveDto reserveDto);
    public IActionResult CreateReserve(ReserveDto reserveDto);
    public DynamicResponse UpdateReserve(ReserveDto reserveDto);
    public IActionResult DeleteReserve(string id);
}
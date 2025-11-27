public class Allocation
{
    public string Id { get; set;}
    public string RoomId { get; set;}
    public string UserId { get; set;}
    public DateTime allocatedDay { get; set;}
    public string allocatedPeriod { get; set;} //periodo que o local foi alocado (manhã, tarde, full)
    public string Code { get; set;} //codigo a ser apresentado
    public DateTime CreatedAt { get; set;}
    public DateTime? UpdatedAt { get; set;}
    public DateTime? DeletedAt { get; set;}
}
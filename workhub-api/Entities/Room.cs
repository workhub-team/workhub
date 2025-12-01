public class Room
{
    public string Id { get; set;}
    public string UnityId { get; set;} //a que unidade do workhub ela pertence
    public string Name { get; set;} //nome da sala
    public int Seats { get; set;} //quantas pessoas podem usar a sala
    public bool IsShared { get; set;} //privado/compartilhado
    public DateTime CreatedAt { get; set;}
    public DateTime? UpdatedAt { get; set;}
    public DateTime? DeletedAt { get; set;}
}
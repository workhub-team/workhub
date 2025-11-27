public class Room
{
    public string Id { get; set;}
    public string FK_LocalId { get; set;} //a que unidade do workhub ela pertence
    public string Seats { get; set;} //quantas pessoas podem usar a sala
    public string Type { get; set;} //privado/compartilhado
    public DateTime CreatedAt { get; set;}
    public DateTime? UpdatedAt { get; set;}
    public DateTime? DeletedAt { get; set;}
}
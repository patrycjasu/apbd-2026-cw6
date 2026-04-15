using cw6.Models;
using cw6.Repositories;

namespace cw6;

public class RoomService(RoomsRepository roomsRepository, ReservationsRepository reservationsRepository)
{
    public IEnumerable<Room> GetRooms()
    {
        return roomsRepository.GetAllRooms();
    }

    public Room GetById(int id)
    {
        var room = roomsRepository.GetRoomById(id);
        if (room is null) throw  new KeyNotFoundException("Room nie istnieje");
        return room;
    }

    public IEnumerable<Room> GetByBuildingCode(string buildingCode)
    {
        IEnumerable<Room> rooms = roomsRepository.GetRoomsByBuildingCode(buildingCode);
        if (!rooms.Any()) throw  new KeyNotFoundException("Brak pokoi w danym budynku");
        return rooms;
    }

    public Room CreateRoom(Room room)
    {
        roomsRepository.AddRoom(room);
        return room;
    }

    public Room UpdateRoom(int id, Room room)
    {
        room.Id = id;
        return !roomsRepository.UpdateRoom(room) ? throw new KeyNotFoundException("Pokoj nie istnieje") : room;
    }

    public void RemoveRoom(int id)
    {
        var room = roomsRepository.GetRoomById(id);
        if (room is null) throw  new KeyNotFoundException("Pokoj nie istnieje");
        
        var czyMaRezerwacje = reservationsRepository.GetAllReservations()
            .Any(r => r.RoomId == id && r.Date > DateOnly.FromDateTime(DateTime.Today));
        if (czyMaRezerwacje) throw new Exception("Nie mozna usunąć bo ma przyszłe rezerwacje");
        roomsRepository.RemoveRoom(room);
    }
    
}
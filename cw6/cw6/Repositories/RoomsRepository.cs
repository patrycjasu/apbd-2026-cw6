using cw6.Models;

namespace cw6.Repositories;

public class RoomsRepository
{
    private static int _nextId = 4;
    public static List<Room> _rooms = new List<Room>()
    {
        new (){Id = 0, Name = "SuperRoom", Capacity = 20, BuildingCode = "A1", Floor = 1, HasProjector = false, IsActive =  true},
        new (){Id = 1, Name = "ScaryRoom", Capacity = 100, BuildingCode = "B1", Floor = 2, HasProjector = true, IsActive =  false},
        new (){Id = 2, Name = "TheBestRoom", Capacity = 50, BuildingCode = "A2",Floor = 2,  HasProjector = true, IsActive =  true},
        new (){Id = 3, Name = "GreatRoom", Capacity = 75, BuildingCode = "A1",Floor = 1,  HasProjector = true, IsActive =  true},
    };

    public IEnumerable<Room> GetAllRooms()
    {
        return _rooms;
    }
    
    public Room? GetRoomById(int id)
    {
        return _rooms.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Room> GetRoomsByBuildingCode(string buildingCode)
    {
        return _rooms.Where(r => r.BuildingCode == buildingCode);
    }

    public void AddRoom(Room room)
    {
        room.Id = _nextId++;
        _rooms.Add(room);
    }
    
    public void RemoveRoom(Room room)
    {
        _rooms.Remove(room);
    }

    public bool UpdateRoom(Room room)
    {
        var CurrentRoom = GetRoomById(room.Id);
        if (CurrentRoom is null)
        {
            return false;
        }
        CurrentRoom.Name = room.Name;
        CurrentRoom.Capacity = room.Capacity;
        CurrentRoom.BuildingCode = room.BuildingCode;
        CurrentRoom.HasProjector = room.HasProjector;
        CurrentRoom.IsActive = room.IsActive;
        CurrentRoom.Floor = room.Floor;
        return true;
    }
    
    
}
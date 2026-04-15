using cw6.Models;

namespace cw6.Repositories;

public class ReservationsRepository
{
    private static int _nextId = 5;
    public static List<Reservation> _reservations = new List<Reservation>()
    {
        new () { Id = 0, RoomId = 0, OrganizerName = "Jan kowalski", Topic = "Topic1", Date = new DateOnly(2026, 1, 1), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(17, 0), Status = "planned"},
        new () { Id = 1, RoomId = 1, OrganizerName = "Maria Nowak", Topic = "Topic2", Date = new DateOnly(2026, 2, 2), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(17, 0), Status = "confirmed"},
        new () { Id = 2, RoomId = 2, OrganizerName = "Jan kowalski", Topic = "Topic3", Date = new DateOnly(2026, 12, 12), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(17, 0), Status = "confirmed"},
        new () { Id = 3, RoomId = 1, OrganizerName = "Maria Nowak", Topic = "Topic4", Date = new DateOnly(2026, 11, 11), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(17, 0), Status = "planned"},
        new () { Id = 4, RoomId = 0, OrganizerName = "XYZ ABC", Topic = "Topic5", Date = new DateOnly(2025, 12, 12), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(17, 0), Status = "canceled"},
    };

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _reservations;
    }

    public Reservation? GetReservation(int id)
    {
        return _reservations.FirstOrDefault(r => r.Id == id);
    }

    public void AddReservation(Reservation reservation)
    {
        reservation.Id = _nextId++;
        _reservations.Add(reservation);
    }

    public void DeleteReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }

    public bool UpdateReservation(Reservation reservation)
    {
        var currRes= GetReservation(reservation.Id);
        if (currRes is null) return false;
        currRes.RoomId = reservation.RoomId;
        currRes.OrganizerName = reservation.OrganizerName;
        currRes.Topic = reservation.Topic;
        currRes.Date = reservation.Date;
        currRes.EndTime = reservation.EndTime;
        currRes.Status = reservation.Status;
        currRes.StartTime = currRes.StartTime;
        return true;
    }
    
    
}
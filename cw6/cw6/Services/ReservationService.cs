using cw6.Models;
using cw6.Repositories;

namespace cw6;

public class ReservationService(ReservationsRepository ReservationsRepository, RoomsRepository RoomsRepository)
{
    public IEnumerable<Reservation> GetAllReservations()
    {
        return ReservationsRepository.GetAllReservations();
    }

    public Reservation ReservationById(int id)
    {
        var reservation = ReservationsRepository.GetReservation(id);
        if (reservation is null) throw new KeyNotFoundException("Rezerwacja nie istnieje");
        return reservation;
    }

    public Reservation CreateReservation(Reservation reservation)
    {
        var room = RoomsRepository.GetRoomById(reservation.RoomId);
        if (room is null) throw new KeyNotFoundException("Nie ma takiego room Id");
        if (!room.IsActive) throw new Exception("Sala jest nieaktywna");
        
        var kolizja = ReservationsRepository.GetAllReservations()
            .Any( r=> r.RoomId == reservation.RoomId
            && r.RoomId == reservation.RoomId
            && r.Date == reservation.Date
            && r.StartTime<reservation.EndTime
            && r.EndTime>reservation.StartTime);
        if (kolizja) throw new Exception("kolizja z inna rezerwacja");
        
        ReservationsRepository.AddReservation(reservation);
        return reservation;
    }

    public void DeleteReservation(int id)
    {
        var reservation = ReservationsRepository.GetReservation(id);
        if (reservation is null) throw new KeyNotFoundException("Rezerwacja nie istnieje");
        ReservationsRepository.DeleteReservation(reservation);
    }

    public Reservation UpdateReservation(int id, Reservation reservation)
    {
        reservation.Id = id;
        return !ReservationsRepository.UpdateReservation(reservation) ? throw new KeyNotFoundException("Rezerwacja nie istnieje") : reservation;
    }
    
}
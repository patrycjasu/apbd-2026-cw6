using cw6.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw6.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ReservationsController(ReservationService service) : ControllerBase
{
    /*
     * Metoda Endpoint Opis
     * GET /api/reservations Zwraca wszystkie rezerwacje.
     * GET /api/reservations/{id} Zwraca jedną rezerwację.
     * GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2 Zwraca rezerwacje przefiltrowane po query stringu.
     * POST /api/reservations Tworzy nową rezerwację.
     * PUT /api/reservations/{id} Aktualizuje istniejącą rezerwację.
     * DELETE /api/reservations/{id} Usuwa rezerwację.
     */
    [HttpGet]
    public IActionResult GetReservations([FromQuery] DateOnly? date,[FromQuery] string? status, [FromQuery] int? roomId)
    {
        var res = service.GetAllReservations();
        if (date.HasValue) res = res.Where(r => r.Date == date.Value);
        if (roomId.HasValue) res = res.Where(r => r.RoomId == roomId.Value);
        if (status != null) res = res.Where(r => r.Status.Equals(status));
        return Ok(res);
    }

    [HttpGet("{id}")]
    public IActionResult GetReservationById([FromRoute]int id)
    {
        try
        {
            return Ok(service.ReservationById(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    

    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation reservation)
    {
        try
        {
            var newRes = service.CreateReservation(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = newRes.Id }, newRes);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e) when (e.Message.Equals("Sala jest nieaktywna"))
        {
            return BadRequest(e.Message);
        }
        catch (Exception e) when (e.Message.Equals("kolizja z inna rezerwacja"))
        {
            return Conflict(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveRoom([FromRoute] int id)
    {
        try
        {
            service.DeleteReservation(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation([FromRoute] int id, [FromBody] Reservation reservation)
    {
        try
        {
            return Ok(service.UpdateReservation(id, reservation));
        } catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    
    
}
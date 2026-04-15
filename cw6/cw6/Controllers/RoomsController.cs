using cw6.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw6.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RoomsController(RoomService service) : ControllerBase
{
    /*
     * Metoda Endpoint Opis
     * GET /api/rooms Zwraca wszystkie sale.
     * GET /api/rooms/{id} Zwraca pojedynczą salę po identyfikatorze.
     * GET /api/rooms/building/{buildingCode} Zwraca sale z wybranego budynku. Parametr buildingCode ma być pobierany z trasy.
     * GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true Zwraca sale przefiltrowane po query stringu.
     * POST /api/rooms Dodaje nową salę.
     * PUT /api/rooms/{id} Aktualizuje pełne dane sali.
     * DELETE /api/rooms/{id} Usuwa salę.
     */
    
    [HttpGet]
    public IActionResult GetRooms([FromQuery] int? minCapacity,[FromQuery] bool? hasProjector,  [FromQuery] bool? activeOnly)
    {
        var rooms = service.GetRooms();
        if (minCapacity.HasValue) rooms = rooms.Where(r=>r.Capacity >= minCapacity.Value);
        if (hasProjector.HasValue) rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
        if (activeOnly.HasValue) rooms = rooms.Where(r => r.IsActive == activeOnly.Value);
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public IActionResult GetRoomById([FromRoute]int id)
    {
        try
        {
            return Ok(service.GetById(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoomByBuildingCode(string buildingCode)
    {
        try
        { 
            return Ok(service.GetByBuildingCode(buildingCode));
        } catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }

    }
    
    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room room)
    {
        var newRoom = service.CreateRoom(room);
        return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, newRoom);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoom([FromRoute] int id, [FromBody] Room room)
    {
        try
        {
            return Ok(service.UpdateRoom(id, room));
        } catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveRoom([FromRoute] int id)
    {
        try
        {
            service.RemoveRoom(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        } catch  (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    
    
    
}
using APBD_PJATK_Cw3_s30650.Data;
using APBD_PJATK_Cw3_s30650.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_PJATK_Cw3_s30650.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetRooms(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = AppData.Rooms.AsQueryable();

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Room> GetRoomById(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetRoomsByBuilding(string buildingCode)
    {
        var rooms = AppData.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<Room> CreateRoom([FromBody] Room room)
    {
        room.Id = AppData.Rooms.Any()
            ? AppData.Rooms.Max(r => r.Id) + 1
            : 1;

        AppData.Rooms.Add(room);

        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Room> UpdateRoom(int id, [FromBody] Room updatedRoom)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        room.Name = updatedRoom.Name;
        room.BuildingCode = updatedRoom.BuildingCode;
        room.Floor = updatedRoom.Floor;
        room.Capacity = updatedRoom.Capacity;
        room.HasProjector = updatedRoom.HasProjector;
        room.IsActive = updatedRoom.IsActive;

        return Ok(room);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        var hasReservations = AppData.Reservations.Any(r => r.RoomId == id);

        if (hasReservations)
            return Conflict("Cannot delete room because it has related reservations.");

        AppData.Rooms.Remove(room);

        return NoContent();
    }
}
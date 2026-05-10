using APBD_PJATK_Cw3_s30650.Data;
using APBD_PJATK_Cw3_s30650.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_PJATK_Cw3_s30650.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetReservations(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var reservations = AppData.Reservations.AsQueryable();

        if (date.HasValue)
            reservations = reservations.Where(r => r.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            reservations = reservations.Where(r =>
                r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId.Value);

        return Ok(reservations.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetReservationById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }
}
using APBD_PJATK_Cw3_s30650.Models;

namespace APBD_PJATK_Cw3_s30650.Data;

public static class AppData
{
    public static List<Room> Rooms { get; set; } = new()
    {
        new Room
        {
            Id = 1,
            Name = "Sala 101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },

        new Room
        {
            Id = 2,
            Name = "Lab 204",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },

        new Room
        {
            Id = 3,
            Name = "Sala 305",
            BuildingCode = "A",
            Floor = 3,
            Capacity = 40,
            HasProjector = false,
            IsActive = true
        }
    };

    public static List<Reservation> Reservations { get; set; } = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Jan Nowak",
            Topic = "C# Basics",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "confirmed"
        },

        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Anna Kowalska",
            Topic = "REST API",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(12, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "planned"
        }
    };
}
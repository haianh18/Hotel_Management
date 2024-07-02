using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class BookingReservation
{
    private static int _nextId = 3; // Biến tĩnh để giữ giá trị tiếp theo của BookingReservationId

    public BookingReservation()
    {
        BookingReservationId = _nextId++;
    }
    public int BookingReservationId { get; set; }

    public DateTime BookingDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public byte? BookingStatus { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;
}

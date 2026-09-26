namespace SrpLab;

public class AppointmentSlotScheduler
{
    private readonly HashSet<DateTimeOffset> _booked = new();
    private readonly AppointmentBusinessHoursPolicy _businessHoursPolicy = new();

    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours,
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        var cursor = Align(from, slotMinutes);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (_businessHoursPolicy.IsWithinBusinessHours(
                    cursor,
                    open,
                    close,
                    slotMinutes)
                && !_booked.Contains(cursor))
            {
                return cursor;
            }

            cursor = cursor.AddMinutes(slotMinutes);
        }

        return null;
    }

    public bool TryBook(
        DateTimeOffset slot,
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        if (!_businessHoursPolicy.IsWithinBusinessHours(
                slot,
                open,
                close,
                slotMinutes)
            || _booked.Contains(slot))
        {
            return false;
        }

        _booked.Add(slot);
        return true;
    }

    private DateTimeOffset Align(DateTimeOffset from, int slotMinutes)
    {
        var minutes = from.Minute - (from.Minute % slotMinutes);

        return new DateTimeOffset(
            from.Year,
            from.Month,
            from.Day,
            from.Hour,
            minutes,
            0,
            from.Offset);
    }
}
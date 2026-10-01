namespace SrpLab;

public class AppointmentBusinessHoursPolicy
{
    public bool IsWithinBusinessHours(
        DateTimeOffset when,
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        // Clinic calendar policy — HR/ops — not the same as ICS serialization.
        if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
            return false;

        var t = TimeOnly.FromDateTime(when.DateTime);

        return t >= open && t.AddMinutes(slotMinutes) <= close;
    }
}
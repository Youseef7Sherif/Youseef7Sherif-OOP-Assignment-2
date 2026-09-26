namespace SrpLab;

/// <summary>
/// Appointment desk: coordinates business-hours policy, slot scheduling,
/// ICS calendar generation, and SMS reminder formatting.
/// </summary>
public sealed class AppointmentDesk
{
    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    private readonly AppointmentBusinessHoursPolicy _businessHoursPolicy = new();
    private readonly AppointmentSlotScheduler _slotScheduler = new();
    private readonly AppointmentIcsFormatter _icsFormatter = new();
    private readonly AppointmentSmsFormatter _smsFormatter = new();

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        return _businessHoursPolicy.IsWithinBusinessHours(
            when,
            Open,
            Close,
            SlotMinutes);
    }

    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours)
    {
        return _slotScheduler.FindNextSlot(
            from,
            searchHours,
            Open,
            Close,
            SlotMinutes);
    }

    public bool TryBook(DateTimeOffset slot)
    {
        return _slotScheduler.TryBook(
            slot,
            Open,
            Close,
            SlotMinutes);
    }

    public string ToIcs(
        DateTimeOffset slot,
        string patientName,
        string clinician)
    {
        return _icsFormatter.Format(
            slot,
            SlotMinutes,
            patientName,
            clinician);
    }

    public string SmsReminder(
        DateTimeOffset slot,
        string clinicPhone)
    {
        return _smsFormatter.Format(slot, clinicPhone);
    }
}
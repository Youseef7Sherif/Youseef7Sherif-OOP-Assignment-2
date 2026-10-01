namespace SrpLab;

public class EscalationBlurbFormatter
{
    public string Format(
        string ticketId,
        string priority,
        DateTimeOffset slaDeadline)
    {
        return $"ESCALATE {ticketId} priority={priority} breachAt={slaDeadline:u} keywords-scanned=yes";
    }
}
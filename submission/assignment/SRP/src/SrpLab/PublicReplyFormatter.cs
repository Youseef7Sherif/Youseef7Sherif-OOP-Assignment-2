namespace SrpLab;

public class PublicReplyFormatter
{
    public string Format(
        string ticketId,
        string agentName,
        string priority,
        DateTimeOffset slaDeadline)
    {
        var apology = priority == "P1"
            ? "We are treating this as a critical incident."
            : "Thanks for reaching out.";

        return $"Hi,\n{apology}\nTicket {ticketId} is with {agentName}. Next update before {slaDeadline:u}.\n";
    }
}
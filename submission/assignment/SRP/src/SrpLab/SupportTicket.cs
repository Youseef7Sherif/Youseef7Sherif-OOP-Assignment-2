namespace SrpLab;

public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    private readonly TicketPriorityCalculator _priorityCalculator = new();
    private readonly SlaDeadlineCalculator _slaDeadlineCalculator = new();
    private readonly PublicReplyFormatter _publicReplyFormatter = new();
    private readonly EscalationBlurbFormatter _escalationBlurbFormatter = new();

    public SupportTicket(
        string id,
        string subject,
        string body,
        DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityCalculator.Calculate(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaDeadlineCalculator.Calculate(OpenedAt, Priority);
    }

    public bool IsBreached(DateTimeOffset now)
    {
        return _slaDeadlineCalculator.IsBreached(now, SlaDeadline());
    }

    public string DraftPublicReply(string agentName)
    {
        return _publicReplyFormatter.Format(
            Id,
            agentName,
            Priority,
            SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _escalationBlurbFormatter.Format(
            Id,
            Priority,
            SlaDeadline());
    }
}
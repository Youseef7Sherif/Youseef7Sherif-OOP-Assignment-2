namespace SrpLab;

public class SlaDeadlineCalculator
{
    public DateTimeOffset Calculate(DateTimeOffset openedAt, string priority)
    {
        var hours = priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };

        return openedAt.AddHours(hours);
    }

    public bool IsBreached(DateTimeOffset now, DateTimeOffset deadline)
    {
        return now > deadline;
    }
}
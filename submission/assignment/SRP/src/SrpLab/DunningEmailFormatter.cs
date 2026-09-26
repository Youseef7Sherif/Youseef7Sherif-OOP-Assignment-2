namespace SrpLab;

public class DunningEmailFormatter
{
    public string Format(
        string customerName,
        DateOnly asOf,
        decimal amount,
        int failedPayments,
        string invoiceNumber)
    {
        var severity = failedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };

        return $"Subject: {severity} {invoiceNumber}\n" +
               $"Hi {customerName},\n" +
               $"Balance {amount:C} as of {asOf:o} ({failedPayments} failures).\n";
    }
}
namespace SrpLab;

public class WmsXmlBatchFormatter
{
    public string Format(
        string batchId,
        IReadOnlyList<(string Sku, int Allocated)> allocations)
    {
        // Integration contract with WMS — separate reason to change.

        var parts = allocations.Select(
            a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");

        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}
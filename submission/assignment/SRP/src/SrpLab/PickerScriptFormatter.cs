namespace SrpLab;

public class PickerScriptFormatter
{
    public string Format(
        IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder,
        IReadOnlyList<(string Sku, int Allocated)> allocations,
        List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        // UX wording for handheld devices — separate owners.

        var steps = walkingOrder
            .Select((s, i) =>
                $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");

        var shortfalls = allocations.Where(a =>
        {
            var line = lines.First(l => l.Sku == a.Sku);

            return a.Allocated < line.QtyNeeded;
        });

        var warning = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls.Select(s => s.Sku))
            : "SHORTAGES: none";

        return string.Join('\n', steps) + "\n" + warning;
    }
}
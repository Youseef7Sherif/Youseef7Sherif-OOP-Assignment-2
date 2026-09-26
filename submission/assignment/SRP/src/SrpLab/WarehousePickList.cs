namespace SrpLab;

/// <summary>
/// Warehouse pick list: coordinates stock allocation, path planning,
/// picker instructions, and WMS batch formatting.
/// </summary>
public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    private readonly StockAllocationCalculator _allocationCalculator = new();
    private readonly WarehousePathPlanner _pathPlanner = new();
    private readonly PickerScriptFormatter _pickerScriptFormatter = new();
    private readonly WmsXmlBatchFormatter _wmsXmlBatchFormatter = new();

    public void AddNeed(
        string sku,
        string aisle,
        int bin,
        int qtyNeeded,
        int qtyOnHand)
    {
        _lines.Add((
            sku,
            aisle,
            bin,
            qtyNeeded,
            qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _allocationCalculator.Allocate(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _pathPlanner.CreateWalkingOrder(_lines);
    }

    public string PickerScript()
    {
        return _pickerScriptFormatter.Format(
            WalkingOrder(),
            Allocate(),
            _lines);
    }

    public string WmsXmlBatch(string batchId)
    {
        return _wmsXmlBatchFormatter.Format(
            batchId,
            Allocate());
    }
}
namespace SrpLab;

public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

    private readonly KitchenAllergenDetector _allergenDetector = new();
    private readonly KitchenReadyTimeCalculator _readyTimeCalculator = new();
    private readonly ThermalTicketFormatter _thermalTicketFormatter = new();

    public void AddItem(
        string item,
        IEnumerable<string> ingredients,
        int prepMinutes)
    {
        _items.Add((
            item,
            ingredients
                .Select(i => i.Trim().ToLowerInvariant())
                .ToList(),
            prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        return _allergenDetector.Detect(_items);
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        var allergens = DetectAllergens();

        return _readyTimeCalculator.Calculate(
            _items,
            openStations,
            allergens.Count);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var allergens = DetectAllergens();
        var estimatedReadyMinutes = EstimatedReadyMinutes(2);

        return _thermalTicketFormatter.Format(
            orderNumber,
            estimatedReadyMinutes,
            _items,
            allergens);
    }

    public string ExpoLaneHint()
    {
        return DetectAllergens().Count > 0
            ? "LANE-ALLERGY"
            : EstimatedReadyMinutes(2) > 20
                ? "LANE-SLOW"
                : "LANE-FAST";
    }
}
namespace SrpLab;

public class PagerAlertLog
{
    private readonly List<string> _pagerLog = new();

    public void AddAlert(int vitalsScore, int bed)
    {
        if (vitalsScore >= 8)
            _pagerLog.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        var copy = _pagerLog.ToList();
        _pagerLog.Clear();
        return copy;
    }
}
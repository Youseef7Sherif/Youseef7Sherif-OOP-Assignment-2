namespace SrpLab;

/// <summary>
/// Hospital ward board: tracks beds, computes acuity scores, drafts nurse handoff notes,
/// and decides which pager code to fire. Looks like "one ward concern" — it is not.
/// </summary>
public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();
    private readonly AcuityScorer _acuityScorer = new AcuityScorer();
    private readonly PagerAlertLog _pagerAlertLog = new PagerAlertLog();
    private readonly HandoffNoteFormatter _handoffNoteFormatter = new HandoffNoteFormatter();
    private readonly WardCensusCsvExporter _wardCensusCsvExporter = new WardCensusCsvExporter();
    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = ScoreAcuity(heartRate, spo2);

        _pagerAlertLog.AddAlert(_vitalsScore[bed], bed);
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        return _acuityScorer.ScoreAcuity(heartRate, spo2);   
    }

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";
        return _handoffNoteFormatter.BuildHandoffNote(bed, _bedPatient[bed], _vitalsScore[bed]);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerAlertLog.DrainPagerLog();
    }

    public string ExportCensusCsv()
    {
        return _wardCensusCsvExporter.ExportCensusCsv(_bedPatient, _vitalsScore);
    }
}

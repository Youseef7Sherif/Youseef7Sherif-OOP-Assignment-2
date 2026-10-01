namespace SrpLab;

public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly GradePolicy _gradePolicy = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();
    private readonly GradeCsvExporter _gradeCsvExporter = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(score));

        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }

        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list) ||
            list.Count == 0)
        {
            return 0m;
        }

        return Math.Round(list.Average(), 2);
    }

    public string Letter(string studentId)
    {
        return _gradePolicy.GetLetter(
            Average(studentId));
    }

    public bool MeetsHonorRoll(string studentId)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);

        return _gradePolicy.MeetsHonorRoll(
            average,
            letter);
    }

    public string TranscriptPlain(
        string studentId,
        string fullName)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);
        var honor = MeetsHonorRoll(studentId);

        return _transcriptFormatter.Format(
            studentId,
            fullName,
            average,
            letter,
            honor);
    }

    public string ExportCsv()
    {
        return _gradeCsvExporter.Export(
            _scores,
            _gradePolicy);
    }
}
namespace SrpLab;

public class WardCensusCsvExporter
{

    public string ExportCensusCsv(Dictionary<int, string> bedPatient, Dictionary<int, int> vitalsScore)
    {
        // Persistence/export shape mixed into the same type as acuity + paging.
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var bed in bedPatient.Keys.OrderBy(x => x))
            lines.Add($"{bed},{bedPatient[bed]},{vitalsScore[bed]}");
        return string.Join('\n', lines);
    }
}


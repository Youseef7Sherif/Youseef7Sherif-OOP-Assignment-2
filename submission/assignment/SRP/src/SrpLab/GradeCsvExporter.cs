namespace SrpLab;

public class GradeCsvExporter
{
    public string Export(
        Dictionary<string, List<decimal>> scores,
        GradePolicy gradePolicy)
    {
        var rows = new List<string>
        {
            "studentId,average,letter,honor"
        };

        foreach (var id in scores.Keys.OrderBy(x => x))
        {
            var list = scores[id];

            var average = list.Count == 0
                ? 0m
                : Math.Round(list.Average(), 2);

            var letter = gradePolicy.GetLetter(average);
            var honor = gradePolicy.MeetsHonorRoll(average, letter);

            rows.Add(
                $"{id},{average},{letter},{(honor ? 1 : 0)}");
        }

        return string.Join('\n', rows);
    }
}
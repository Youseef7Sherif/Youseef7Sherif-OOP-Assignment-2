namespace SrpLab;

public class GradePolicy
{
    public string GetLetter(decimal average)
    {
        if (average >= 90)
            return "A";

        if (average >= 80)
            return "B";

        if (average >= 70)
            return "C";

        if (average >= 60)
            return "D";

        return "F";
    }

    public bool MeetsHonorRoll(decimal average, string letter)
    {
        return average >= 85 && letter is "A" or "B";
    }
}
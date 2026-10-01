namespace SrpLab;

public class TranscriptFormatter
{
    public string Format(
        string studentId,
        string fullName,
        decimal average,
        string letter,
        bool honor)
    {
        return $"TRANSCRIPT\n" +
               $"Student: {fullName} ({studentId})\n" +
               $"Average: {average}\n" +
               $"Letter: {letter}\n" +
               $"Honor: {honor}\n";
    }
}
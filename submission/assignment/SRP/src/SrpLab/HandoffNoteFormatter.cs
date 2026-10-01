namespace SrpLab;

public class HandoffNoteFormatter
{

    public string BuildHandoffNote(int bed, string patient, int acuity)
    {
        if (string.IsNullOrWhiteSpace(patient))
            return $"Bed {bed}: empty";

        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        // Presentation / narrative format will change without clinical rules changing.
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }

}


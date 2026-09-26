namespace SrpLab;

public sealed class LoanDesk
{
    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    private readonly LoanRiskCalculator _riskCalculator = new();
    private readonly LoanEligibilityChecker _eligibilityChecker = new();
    private readonly LoanDocumentsCalculator _documentsCalculator = new();
    private readonly LoanDecisionLetterFormatter _decisionLetterFormatter = new();
    private readonly LoanUnderwriterCsvExporter _csvExporter = new();

    public LoanDesk(
        decimal requestedAmount,
        int creditScore,
        int employmentMonths,
        bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }

    public decimal RiskScore()
    {
        return _riskCalculator.Calculate(
            RequestedAmount,
            CreditScore,
            EmploymentMonths,
            HasCollateral);
    }

    public bool IsEligible()
    {
        return _eligibilityChecker.IsEligible(
            RiskScore(),
            CreditScore);
    }

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _documentsCalculator.Calculate(
            RequestedAmount,
            EmploymentMonths,
            HasCollateral,
            IsEligible());
    }

    public string DecisionLetter(string applicantName)
    {
        return _decisionLetterFormatter.Format(
            applicantName,
            RequestedAmount,
            RiskScore(),
            IsEligible(),
            RequiredDocuments());
    }

    public string UnderwriterCsvRow(string applicationId)
    {
        return _csvExporter.Export(
            applicationId,
            CreditScore,
            EmploymentMonths,
            HasCollateral,
            RiskScore(),
            IsEligible());
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erm.DataAccess;

public sealed class RiskProfile
{
    private int _occurrenceProbability;
    private int _potentialBusinessImpact;

    public int Id { get; set; }
    public int BusinessProcessId { get; set; }
    
    public required string RiskName { get; set; }
    public string? Description { get; set; }
    public required BusinessProcess BusinessProcess { get; set; } // связанные бизнес-процессы

    public required int OccurrenceProbability // вероятность риска 0-10
    {
        get => _occurrenceProbability;
        set => _occurrenceProbability = (value < 1 || value > 10)
            ? throw new ArgumentOutOfRangeException(nameof(value))
            : value;
    }

    public required int PotentialBusinessImpact // влияние риска 0-10
    {
        get => _potentialBusinessImpact;
        set => _potentialBusinessImpact = (value < 1 || value > 10)
            ? throw new ArgumentOutOfRangeException(nameof(value))
            : value;
    }

    public string? PotentialSolution { get; set; } // решение риска
}
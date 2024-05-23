using Erm.DataAccess;

namespace Erm.BusinessLayer;

internal static class RiskProfileInfoExtension
{
    internal static RiskProfile ToRiskProfile(this RiskProfileInfo profileInfo)
        => new()
        {
            RiskName = profileInfo.Name,
            Description = profileInfo.Description,
            BusinessProcess =
                new() { Name = profileInfo.BusinessProcess, Domain = profileInfo.BusinessProcess },
            PotentialBusinessImpact = profileInfo.PotentialBusinessImpact,
            OccurrenceProbability = profileInfo.OccurrenceProbability
        };
}
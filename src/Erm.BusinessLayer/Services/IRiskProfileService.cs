using System.Diagnostics;

using Erm.DataAccess;


namespace Erm.BusinessLayer.Services;

public interface IRiskProfileService
{
    Task CreateAsync(RiskProfileInfo profileInfo, CancellationToken token = default);
    Task<IEnumerable<RiskProfileInfo>> QueryAsync(string query, CancellationToken token = default);
    Task<RiskProfileInfo> GetAsync(string name, CancellationToken token = default);
    Task UpdateAsync(string name, RiskProfile riskProfile, CancellationToken token = default);
    Task DeleteAsync(string name, CancellationToken token = default);
}
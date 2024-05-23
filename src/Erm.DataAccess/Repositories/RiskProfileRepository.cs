using Microsoft.EntityFrameworkCore;

namespace Erm.DataAccess.Repositories;

public sealed class RiskProfileRepository : IRiskProfileRepository
{
    private readonly ErmDbContext _db = new();

    public async Task CreateAsync(RiskProfile entity, CancellationToken token = default)
    {
        await _db.RiskProfiles.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(string name, CancellationToken token = default)
    {
        await _db.RiskProfiles.Where(x => x.RiskName.Equals(name)).ExecuteDeleteAsync(token);
        await _db.SaveChangesAsync(token);
    }

    public Task<RiskProfile> GetAsync(string name, CancellationToken token = default) => _db.RiskProfiles
        .AsNoTracking()
        .Include(i => i.BusinessProcess)
        .SingleAsync(x => x.RiskName.Equals(name), token);

    public async Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default)
        => await _db.RiskProfiles
            .AsNoTracking()
            .Include(i => i.BusinessProcess)
            .Where(x => x.RiskName.Contains(query) || x.Description.Contains(query)).ToArrayAsync(token);


    public async Task UpdateAsync(string name, RiskProfile riskProfile, CancellationToken token = default)
    {
        RiskProfile profileToUpdate = await _db.RiskProfiles.SingleAsync(x => x.RiskName.Equals(name), token);

        profileToUpdate.RiskName = riskProfile.RiskName;
        profileToUpdate.Description = riskProfile.Description;
        profileToUpdate.PotentialBusinessImpact = riskProfile.PotentialBusinessImpact;
        profileToUpdate.PotentialSolution = riskProfile.PotentialSolution;
        profileToUpdate.OccurrenceProbability = riskProfile.OccurrenceProbability;

        await _db.SaveChangesAsync(token);
    }
}
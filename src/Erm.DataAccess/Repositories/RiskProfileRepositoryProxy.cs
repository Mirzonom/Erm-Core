using System.Text.Json;

using StackExchange.Redis;

namespace Erm.DataAccess.Repositories;

public sealed class RiskProfileRepositoryProxy : IRiskProfileRepository
{
    private readonly RiskProfileRepository _originalRepository;
    private readonly IDatabase _redisDb;
    private const string RedisHost = "127.0.0.1:6379";


    public RiskProfileRepositoryProxy(RiskProfileRepository originalRepository)
    {
        _originalRepository = originalRepository;

        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(RedisHost);
        _redisDb = connectionMultiplexer.GetDatabase();
    }

    public Task CreateAsync(RiskProfile entity, CancellationToken token) =>
        _originalRepository.CreateAsync(entity, token);

    public Task DeleteAsync(string name, CancellationToken token) => _originalRepository.DeleteAsync(name, token);

    public async Task<RiskProfile> GetAsync(string name, CancellationToken token)
    {
        RedisValue redisValue = await _redisDb.StringGetAsync(name);
        if (redisValue.IsNullOrEmpty)
        {
            RiskProfile profileFromDb = await _originalRepository.GetAsync(name, token);
            string redisProfileJson = JsonSerializer.Serialize(profileFromDb);

            await _redisDb.StringSetAsync(name, redisProfileJson);

            return profileFromDb;
        }

        string redisProfileJsonStr = redisValue.ToString();

        RiskProfile profile = JsonSerializer.Deserialize<RiskProfile>(redisProfileJsonStr)
                              ?? throw new InvalidOperationException();
        return profile;
    }

    public Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default)
        => _originalRepository.QueryAsync(query, token);


    public Task UpdateAsync(string name, RiskProfile riskProfile, CancellationToken token = default)
        => _originalRepository.UpdateAsync(name, riskProfile, token);
}
using AutoMapper;

using Erm.BusinessLayer.Validators;
using Erm.DataAccess;
using Erm.DataAccess.Repositories;

using FluentValidation;

namespace Erm.BusinessLayer.Services;

public sealed class RiskProfileService : IRiskProfileService
{
    private readonly IRiskProfileRepository _repository;
    private readonly IMapper _mapper;
    private readonly RiskProfileInfoValidator _validationRules;
    private IRiskProfileService _riskProfileServiceImplementation;


    public RiskProfileService()
    {
        _validationRules = new();
        _repository = new RiskProfileRepositoryProxy(new RiskProfileRepository());
        _mapper = AutoMapperHelper.MapperConfiguration.CreateMapper();
    }

    public async Task CreateAsync(RiskProfileInfo profileInfo, CancellationToken token = default)
    {
        await _validationRules.ValidateAndThrowAsync(profileInfo, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(profileInfo);
        await _repository.CreateAsync(riskProfile, token);
    }

    public async Task<RiskProfileInfo> GetAsync(string name, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return _mapper.Map<RiskProfileInfo>(await _repository.GetAsync(name, token));
    }

    public Task UpdateAsync(string name, RiskProfile riskProfile, CancellationToken token = default)
    {
        return _riskProfileServiceImplementation.UpdateAsync(name, riskProfile, token);
    }


    public async Task<IEnumerable<RiskProfileInfo>> QueryAsync(string query, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);
        return _mapper.Map<IEnumerable<RiskProfileInfo>>(await _repository.QueryAsync(query, token));
    }

    public async Task UpdateAsync(string name, RiskProfileInfo profileInfo, CancellationToken token = default)
    {
        await _validationRules.ValidateAndThrowAsync(profileInfo, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(profileInfo);
        await _repository.UpdateAsync(name, riskProfile, token);
    }

    public async Task DeleteAsync(string name, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        await _repository.DeleteAsync(name, token);
    }
}
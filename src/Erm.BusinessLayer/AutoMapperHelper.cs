using AutoMapper;

using Erm.BusinessLayer.Mapper;
using Erm.DataAccess;

namespace Erm.BusinessLayer;

internal static class AutoMapperHelper
{
    internal readonly static MapperConfiguration MapperConfiguration = new(
        opt =>
        {
            opt.AddProfile<RiskProfileInfoProfile>();
        });
}
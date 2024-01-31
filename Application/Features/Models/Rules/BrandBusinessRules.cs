using Application.Features.Models.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Models.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandNameCannotBeDuplicatedWhenInserted(string name)
    {
        var result = await _brandRepository.GetAsync(b => b.Name.ToLower() == name.ToLower());
        if (result != null) throw new BusinessException(BrandsMessages.BrandNameExist);
    }
}
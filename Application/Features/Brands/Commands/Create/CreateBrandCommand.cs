using Application.Features.Models.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>,
    ITransactionalRequest, ICacheRemoverRequest, ILoggableRequest
{
    public string Name { get; set; }

    public string? CacheKey => "";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetBrands";

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
            BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

            var brand = _mapper.Map<Brand>(request);
            brand.Id = Guid.NewGuid();

            await _brandRepository.AddAsync(brand);

            var createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);
            return createdBrandResponse;
        }
    }
}
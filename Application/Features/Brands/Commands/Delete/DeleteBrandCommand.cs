using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeleteBrandResponse>, ICacheRemoverRequest
{
    public Guid Id { get; set; }
    public string? CacheKey => "";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetBrands";

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<DeleteBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken);
            await _brandRepository.DeleteAsync(brand);
            var response = new DeleteBrandResponse { IsSuccess = true };

            return response;
        }
    }
}
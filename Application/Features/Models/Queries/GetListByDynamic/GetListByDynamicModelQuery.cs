using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListByDynamic;

public class GetListByDynamicModelQuery : IRequest<GetListResponse<GetListByDynamicModelListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListByDynamicModelQueryHandler : IRequestHandler<GetListByDynamicModelQuery,
        GetListResponse<GetListByDynamicModelListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public GetListByDynamicModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicModelListItemDto>> Handle(GetListByDynamicModelQuery request,
            CancellationToken cancellationToken)
        {
            var models = await _modelRepository.GetListByDynamicAsync(
                request.DynamicQuery,
                include: x => x.Include(x => x.Brand).Include(x => x.Fuel).Include(x => x.Transmission),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
            );
            var response = _mapper.Map<GetListResponse<GetListByDynamicModelListItemDto>>(models);
            return response;
        }
    }
}
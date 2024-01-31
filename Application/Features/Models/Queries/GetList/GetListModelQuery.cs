using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListModelListItemDto>> Handle(GetListModelQuery request,
            CancellationToken cancellationToken)
        {
            var models = await _modelRepository.GetListAsync(
                include: x => x.Include(x => x.Brand).Include(x => x.Fuel).Include(x => x.Transmission),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
            );
            var response = _mapper.Map<GetListResponse<GetListModelListItemDto>>(models);
            return response;
        }
    }
}
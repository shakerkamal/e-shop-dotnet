using AutoMapper;
using EShop.Contracts;
using EShop.Shared.DataTransferObjects.ProductDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductIndexDto>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductIndexDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repositoryManager.Product.GetAllAsync(cancellationToken);
        var response = _mapper.Map<IEnumerable<ProductIndexDto>>(products);

        return response;
    }
}

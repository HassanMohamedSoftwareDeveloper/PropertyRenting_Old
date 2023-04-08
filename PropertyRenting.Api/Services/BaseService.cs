using AutoMapper;
using PropertyRenting.Api.UOW;

namespace PropertyRenting.Api.Services;

public abstract class BaseService
{
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWork UnitOfWork;

    public BaseService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
}

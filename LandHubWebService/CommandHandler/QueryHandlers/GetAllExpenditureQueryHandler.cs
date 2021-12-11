using AutoMapper;

using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllExpenditureQueryHandler : IRequestHandler<GetAllExpenditureQuery, List<ExpenditureForUi>>
    {

        private readonly IBaseRepository<Expenditure> _baseRepositoryExpenditure;
        private readonly IMapper _mapper;


        public GetAllExpenditureQueryHandler(IMapper mapper, IBaseRepository<Expenditure> baseRepositoryExpenditure)
        {
            _mapper = mapper;
            _baseRepositoryExpenditure = baseRepositoryExpenditure;
        }

        public async Task<List<ExpenditureForUi>> Handle(GetAllExpenditureQuery request, CancellationToken cancellationToken)
        {
            var expenditureForList = new List<ExpenditureForUi>();
            var expenditureList = await _baseRepositoryExpenditure.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);
            if (request.SearchKey == null || request.SearchKey == "")
            {
                foreach (var expenditure in expenditureList.ToList())
                {
                    var expenditureForUi = new ExpenditureForUi()
                    {
                        Id = expenditure.Id,
                        OrgId = expenditure.OrgId,
                        Description = expenditure.Description,
                        Type = expenditure.Type,
                        Amount = expenditure.Amount,
                        Status = expenditure.Status,
                        CreatedDate = expenditure.CreatedDate
                    };

                    expenditureForList.Add(expenditureForUi);
                }
            } else
            {
                foreach (var expenditure in expenditureList.ToList())
                {
                    if (expenditure.Description.Contains(request.SearchKey))
                    {
                        var expenditureForUi = new ExpenditureForUi()
                        {
                            Id = expenditure.Id,
                            OrgId = expenditure.OrgId,
                            Description = expenditure   .Description,
                            Type = expenditure.Type,
                            Amount = expenditure.Amount,
                            Status = expenditure.Status,
                            CreatedDate = expenditure.CreatedDate
                        };

                        expenditureForList.Add(expenditureForUi);
                    }
                }
            }

            return expenditureForList;
        }
    }
}

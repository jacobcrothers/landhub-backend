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
    public class GetAllIncomeQueryHandler : IRequestHandler<GetAllIncomeQuery, List<IncomeForUi>>
    {

        private readonly IBaseRepository<Income> _baseRepositoryIncome;
        private readonly IMapper _mapper;


        public GetAllIncomeQueryHandler(IMapper mapper, IBaseRepository<Income> baseRepositoryIncome)
        {
            _mapper = mapper;
            _baseRepositoryIncome = baseRepositoryIncome;
        }

        public async Task<List<IncomeForUi>> Handle(GetAllIncomeQuery request, CancellationToken cancellationToken)
        {
            var incomeForList = new List<IncomeForUi>();
            var incomeList = await _baseRepositoryIncome.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);
            if (request.SearchKey == null || request.SearchKey == "")
            {
                foreach (var income in incomeList.ToList())
                {

                    var incomeForUi = new IncomeForUi()
                    {
                        Id = income.Id,
                        OrgId = income.OrgId,
                        Description = income.Description,
                        Type = income.Type,
                        Amount = income.Amount,
                        Status = income.Status,
                        CreatedDate = income.CreatedDate
                    };

                    incomeForList.Add(incomeForUi);
                }
            } else
            {
                foreach (var income in incomeList.ToList())
                {
                    if (income.Description.Contains(request.SearchKey)) {
                        var incomeForUi = new IncomeForUi()
                        {
                            Id = income.Id,
                            OrgId = income.OrgId,
                            Description = income.Description,
                            Type = income.Type,
                            Amount = income.Amount,
                            Status = income.Status,
                            CreatedDate = income.CreatedDate
                        };

                        incomeForList.Add(incomeForUi);
                    }
                }
            }

            return incomeForList;
        }
    }
}

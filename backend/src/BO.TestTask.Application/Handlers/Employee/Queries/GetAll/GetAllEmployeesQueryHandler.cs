using System.Linq.Expressions;
using static System.StringComparison;

namespace BO.TestTask.Application.Handlers.Employee.Queries.GetAll;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, GetAllEmployeesQueryResponse>
{
    private readonly AppDbContext _dbContext;

    public GetAllEmployeesQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAllEmployeesQueryResponse> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<EmployeeEntity> query = _dbContext.Employees.Skip(request.Skip * request.Take).Take(request.Take);

        query = OrderQuery(query, request.SortColumn, request.SortOrder);

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name, OrdinalIgnoreCase));
        }

        if (request.DateOfBirth.HasValue)
        {
            query = query.Where(x => x.DateOfBirth == request.DateOfBirth.Value);
        }

        if (request.Married.HasValue)
        {
            query = query.Where(x => x.Married == request.Married.Value);
        }

        if (!string.IsNullOrEmpty(request.Phone))
        {
            query = query.Where(x => x.Name.Contains(request.Phone, OrdinalIgnoreCase));
        }

        if (request.Salary.HasValue)
        {
            query = query.Where(x => x.Salary == request.Salary.Value);
        }

        var result = await query.ToListAsync(cancellationToken);
        var totalCount = await _dbContext.Employees.CountAsync(cancellationToken);

        return new GetAllEmployeesQueryResponse(result, totalCount);
    }

    /// <summary>
    /// Applies ordering to query by provided property name.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="sortColumn"></param>
    /// <param name="sortOrder"></param>
    /// <returns></returns>
    private static IQueryable<EmployeeEntity> OrderQuery(IQueryable<EmployeeEntity> query, string? sortColumn, string sortOrder)
    {
        if (!string.IsNullOrEmpty(sortColumn))
        {
            var type = typeof(EmployeeEntity);
            var sortProperty = type.GetProperties().FirstOrDefault(p => p.Name.Equals(sortColumn, OrdinalIgnoreCase));

            if (sortProperty != null)
            {
                var sortPropertyType = sortProperty.PropertyType;
                var operation = sortOrder.Equals("Asc", OrdinalIgnoreCase) ? "OrderBy" : "OrderByDescending";

                var method = typeof(Queryable).GetMethods().First(m => m.Name == operation);

                var parameter = Expression.Parameter(type);
                var propAccessor = Expression.Property(parameter, sortProperty);

                var lambda = Expression.Lambda(
                    Expression.GetFuncType(type, sortPropertyType),
                    propAccessor,
                    parameter);

                if (method.MakeGenericMethod(type, sortPropertyType).Invoke(null, [query, lambda]) is IOrderedQueryable<EmployeeEntity> res)
                {
                    query = res;
                }
            }
        }

        return query;
    }
}

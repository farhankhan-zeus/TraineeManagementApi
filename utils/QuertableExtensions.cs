
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.utils;

public static class QueryableExtensions
{
    public static IQueryable<Trainee> ApplyPagination(this IQueryable<Trainee> query, int pageNumber, int pageSize)
    {
        return query
             .OrderBy(t => t.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
    public static IQueryable<Trainee> ApplySearch(this IQueryable<Trainee> query, string? search)
{
    if (string.IsNullOrWhiteSpace(search))
        {
            
        return query;
        }

    return query.Where(m =>
        EF.Functions.Like(m.FirstName, $"%{search}%") ||
        EF.Functions.Like(m.LastName, $"%{search}%")||
        EF.Functions.Like(m.Email, $"%{search}%") ||
        EF.Functions.Like(m.TechStack, $"%{search}%")) ;
}
    public static IQueryable<Trainee> ApplyStatusFilter(this IQueryable<Trainee> query, string? status)
{
    if (string.IsNullOrWhiteSpace(status))
        return query;

    return query.Where(m =>m.Status == status       
       ) ;
}
    

}
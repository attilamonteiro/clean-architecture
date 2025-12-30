using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.GetAll;

internal sealed class GetAllUsersQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllUsersQuery, IReadOnlyList<UserResponse>>
{
    public async Task<Result<IReadOnlyList<UserResponse>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        List<UserResponse> users = await context.Users
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .ToListAsync(cancellationToken);

        return users;
    }
}

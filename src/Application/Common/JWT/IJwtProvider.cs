using Domain.Entities;

namespace Application.Common.JWT;

public interface IJwtProvider
{
    string Generate(User user); 
}

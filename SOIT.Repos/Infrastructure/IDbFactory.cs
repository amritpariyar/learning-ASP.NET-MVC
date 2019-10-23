using SOIT.Data;
using System;

namespace SOIT.Repos.Infrastructure
{
    public interface IDbFactory:IDisposable
    {
        SOITEntities Init();
    }
}

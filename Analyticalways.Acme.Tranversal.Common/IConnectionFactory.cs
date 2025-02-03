using System.Data;

namespace Analyticalways.Acme.Tranversal.Common
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}

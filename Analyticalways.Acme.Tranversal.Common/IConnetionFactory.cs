using System.Data;

namespace Analyticalways.Acme.Tranversal.Common
{
    public interface IConnetionFactory
    {
        IDbConnection GetConnection { get; }
    }
}

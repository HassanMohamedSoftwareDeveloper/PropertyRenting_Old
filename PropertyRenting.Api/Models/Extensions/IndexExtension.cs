using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using System.Text;

namespace PropertyRenting.Api.Models.Extensions;

public static class IndexExtension
{
    public static IndexBuilder Include<TEntity>(this IndexBuilder indexBuilder, Expression<Func<TEntity, object>> indexExpression)
    {
        var includeStatement = new StringBuilder();
        foreach (var column in indexExpression.GetPropertyAccessList())
        {
            if (includeStatement.Length > 0) includeStatement.Append(", ");
            includeStatement.AppendFormat("[{0}]", column.Name);
        }
        indexBuilder.HasAnnotation("SqlServer:IncludeIndex", includeStatement.ToString());
        return indexBuilder;
    }
}

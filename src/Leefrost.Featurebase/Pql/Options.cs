using System.Text;

namespace Leefrost.Featurebase.Pql;
public class Options : Query
{
    private readonly Query _query;
    private readonly List<uint> _shards = [];

    public Options(Query query)
    {
        _query = query;
    }

    public Options(Query query, IEnumerable<uint> shards)
        : this(query)
    {
        _shards.AddRange(shards);
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append($"Options({_query.Build()}");

        if (_shards.Count > 0)
            builder.Append($", shards=[{string.Join(',', _shards)}]");

        builder.Append(')');
        return builder.ToString();
    }
}

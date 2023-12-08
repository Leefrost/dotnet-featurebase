using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leefrost.Featurebase.Clients.Community.Raws;

internal class EnumerationResponse
{
    public IEnumerable<EnumerationResult> Results { get; set; }
}

internal class EnumerationResult
{
    public IEnumerable<Field> Fields { get; set; }
    public IEnumerable<Column> Columns { get; set; }
    public IEnumerable<Key> Keys { get; set; }
}

public class Field;
public class Column;
public class Key;


namespace NTier.Logic.Services.Models;

public class GenericResultSet<T> : StandardResultObject
{
    public T? ResultSet { get; set; }
}

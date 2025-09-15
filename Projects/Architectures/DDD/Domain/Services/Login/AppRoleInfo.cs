namespace Domain.Services.Login;

public sealed class AppRoleInfo
{
    public readonly string ARMUrl;

    public readonly PersonInfo Parent;

    private readonly string _Name;

    public readonly bool IsRestricted;

    internal AppRoleInfo(string name, string armUrl, PersonInfo parent, bool isRestricted)
    {
        _Name = name;
        ARMUrl = armUrl;
        Parent = parent;
        IsRestricted = isRestricted;
    }

    public string Name
    {
        get { return _Name; }
    }

    public string ID
    {
        get { return Name; }
    }
}

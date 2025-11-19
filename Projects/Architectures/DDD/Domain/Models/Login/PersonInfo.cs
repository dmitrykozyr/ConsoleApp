using Domain.Interfaces.Login;
using System.Collections;

namespace Domain.Models.Login;

public class PersonInfo
{
    public readonly string _login;

    public readonly string _firstName;

    public readonly string? _middleName;

    public readonly string _lastName;

    public readonly string _fullFIO;

    public readonly string _shortFIO;

    public readonly bool _isLogOn;

    public readonly bool _isEmployeeHeadBranch;

    private readonly Hashtable _rolesHash = [];

    private readonly IProvider _provider;

    public AppRoleInfo[]? Roles { get; set; }

    public PersonInfo(string login, string firstName, string middleName, string lastName, bool isLogOn, bool isEmployeeHeadBranch, IProvider provider)
    {
        _login = login;
        _firstName = firstName;
        _middleName = middleName;
        _lastName = lastName;

        _fullFIO =
            _lastName + " " +
            _firstName + (_middleName is not null ? " " + _middleName : string.Empty);

        _shortFIO =
            _lastName + " " +
            _firstName[..1] + "." +
            (!string.IsNullOrEmpty(_middleName) ? _middleName[..1] + "." : string.Empty);

        _isLogOn = isLogOn;
        _isEmployeeHeadBranch = isEmployeeHeadBranch;
        _provider = provider;

        LoadChilds();
    }

    public void LoadChilds()
    {
        Roles = _provider.GetPersonAppRoles(this);

        _rolesHash.Clear();

        foreach (AppRoleInfo role in Roles)
        {
            _rolesHash.Add(role.ID, role);
        }
    }
}

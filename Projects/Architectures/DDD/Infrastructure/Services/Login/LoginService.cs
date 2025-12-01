using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Login;
using Domain.Models.Login;
using Domain.Models.Options;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace Infrastructure.Services.Login;

public class LoginService : ILoginService
{
    private readonly ISqlService _sqlService;
    private readonly IProvider _provider;

    public static LoginOptions? LoginOptions { get; set; }

    private const string GET_LOGIN_BY_NOTES_NAME = "";

    public LoginService(ISqlService sqlService, IProvider provider, IOptions<LoginOptions> loginOptions)
    {
        _sqlService = sqlService;
        _provider = provider;
        LoginOptions = loginOptions.Value;
    }

    public bool AuthenticateDomainUser()
    {
        //! try catch

        return true; //!


        Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

        IIdentity? claimsIdentity = Thread.CurrentPrincipal?.Identity;

        Guard.IsNotNull(claimsIdentity);

        if (claimsIdentity.IsAuthenticated &&
            !string.IsNullOrEmpty(claimsIdentity.Name))
        {
            PersonInfo? result = GetLogins(claimsIdentity.Name);

            if (result is not null && result.Roles is not null)
            {
                Guard.IsNotNull(LoginOptions);
                Guard.IsNotNull(LoginOptions.RolesAllowed);

                bool allowedRoles = LoginOptions.RolesAllowed.Any(r => result.Roles.Any(role => role.ID == r));

                if (allowedRoles)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private PersonInfo? GetLogins(string notesName)
    {
        List<string>? logins = GetPersonLogins(notesName);

        string? systemLogin = logins?.FirstOrDefault(z => z == "_system");

        if (systemLogin is not null)
        {
            var personInfo = GetPersonInfo(systemLogin);

            return personInfo;
        }

        return null;
    }

    private List<string>? GetPersonLogins(string notesName)
    {
        var logins = new List<string>();

        using (SqlConnection? connection = _sqlService.CreateConnection())
        {
            if (connection is null)
            {
                return null;
            }

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GET_LOGIN_BY_NOTES_NAME;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@i_NotesName", notesName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logins.Add(reader.GetString(0));
                    }
                }
            }

            return logins;
        }
    }

    public PersonInfo? GetPersonInfo(string login)
    {
        using (SqlConnection? connection = _sqlService.CreateConnection())
        {
            Guard.IsNotNull(connection);

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    string.Format(
                        @"DECLARE @cookie VARBINARY(100); EXECUTE AS LOGIN = '{0}' WITH COOKIE INTO @cookie; exec spGetCurrentPersonInfo; REVERT WITH COOKIE = @cookie;",
                        login);

                command.CommandType = CommandType.Text;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var result = new PersonInfo(
                            login: reader["Login"].ToString() ?? "",
                            firstName: reader["FName"].ToString() ?? "",
                            middleName: reader["MName"].ToString() ?? "",
                            lastName: reader["LName"].ToString() ?? "",
                            isLogOn: (bool)reader["IsLogOn"],
                            isEmployeeHeadBranch: (bool)reader["IsEmployeeHeadBranch"],
                            _provider);

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}

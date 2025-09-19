using Domain.Interfaces;
using Domain.Interfaces.Login;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Services.Login;

public class Provider : IProvider
{
    private readonly ISqlService _sqlService;

    public Provider(ISqlService sqlService)
    {
        _sqlService = sqlService;
    }

    public AppRoleInfo[] GetPersonAppRoles(PersonInfo person)
    {
        var array = new ArrayList();

        using (SqlConnection connection = _sqlService.CreateConnection())
        using (UserContextCommand command = _sqlService.CreateCommand("spGetCurrentPersonAppRoles", CommandType.StoredProcedure))
        using (SqlDataReader reader = command.ExecuteReader(person._login))
        {
            while (reader.Read())
            {
                var appRoleInfo = new AppRoleInfo(
                    name:   reader.GetString(0),
                    armUrl: reader.GetString(1),
                    parent: person,
                    isRestricted: reader.GetBoolean(3));

                array.Add(appRoleInfo);
            }
        }

        var result = (AppRoleInfo[])array.ToArray(typeof(AppRoleInfo));

        return result;
    }
}

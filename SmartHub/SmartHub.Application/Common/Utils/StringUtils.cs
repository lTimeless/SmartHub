using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SmartHub.Application.Common.Utils
{
    public class StringUtils
    {
        public static string CreateConnectionString(IConfiguration configuration)
        {
            var oldConnectionString = configuration["ConnectionStrings:sqlConnection"];
            var argsUser = configuration.GetValue<string>("Pgsql_User");
            var argsPwd = configuration.GetValue<string>("Pgsql_pwd");
            var dictionary = oldConnectionString
                .Remove(oldConnectionString.Length - 1)
                .Split(";")
                .Select(x => x.Split("="))
                .ToDictionary(x => x[0], x => x[1]);
            if (dictionary.ContainsKey("User ID"))
            {
                dictionary["User ID"] = argsUser;
            }
            if (dictionary.ContainsKey("Password"))
            {
                dictionary["Password"] = argsPwd;
            }
            return string.Concat(
                string.Join(";",dictionary.Select(x => x.Key + "=" + x.Value)),
                ";" );
        }
    }
}
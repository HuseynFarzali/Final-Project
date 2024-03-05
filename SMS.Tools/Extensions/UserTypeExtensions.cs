using SMS.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Tools.Extensions
{
    public static class UserTypeExtensions
    {
        public static string GetName(this UserType type)
            => type switch
            {
                UserType.Student => "Student",
                UserType.Teacher => "Teacher",
                _ => throw new Exception($"Type[{type}] is not registered."),
            };

        public static UserType GetUserType(this string name)
            => name.ToLower() switch
            {
                "Student" => UserType.Student,
                "Teacher" => UserType.Teacher,
                _ => throw new Exception($"UserType-name[{name}] is not registered."),
            };
    }
}

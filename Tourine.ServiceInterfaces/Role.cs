using System;
using System.Collections.Generic;

namespace Tourine.ServiceInterfaces
{
    [Flags]
    public enum Role
    {
        Admin = 1,
        Operator = 2,
        Agency = 4
    }

    public static class RoleExtentions
    {
        public static List<T> ParseRole<T>(this Role role)
        {
            List<T> roles = new List<T>();
            foreach (Role r in Enum.GetValues(typeof(Role)))
            {
                if ((role & r) == r)
                {
                    if (typeof(T) == typeof(String))
                    {
                        roles.Add((T)(object)r.ToString());
                    }
                    else if (typeof(T) == typeof(Role))
                    {
                        roles.Add((T)(object) r);
                    }
                }
            }
            return roles;
        }
    }
}

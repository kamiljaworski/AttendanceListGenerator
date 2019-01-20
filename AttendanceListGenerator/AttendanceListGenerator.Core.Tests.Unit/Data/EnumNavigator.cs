using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    public static class EnumNavigator<T> where T : struct
    {
        private static readonly T[] _arrayOfEnums = GetArrayOfEnums();

        private static T Next(T currentEnum) => throw new NotImplementedException();

        private static T Previous(T currentEnum) => throw new NotImplementedException();

        private static T[] GetArrayOfEnums()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"{typeof(T)} is not an Enum!");

            return (T[])Enum.GetValues(typeof(T));
        }
    }
}

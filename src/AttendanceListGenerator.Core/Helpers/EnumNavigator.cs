using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceListGenerator.Core.Helpers
{
    public static class EnumNavigator<T> where T : struct, IConvertible
    {
        private static readonly T[] _arrayOfEnums = GetArrayOfEnums();

        public static T Next(T currentEnum)
        {
            int index = Array.IndexOf(_arrayOfEnums, currentEnum);

            if (index == _arrayOfEnums.Length - 1)
                return _arrayOfEnums[0];

            return _arrayOfEnums[index + 1];
        }

        public static T Previous(T currentEnum)
        {
            int index = Array.IndexOf(_arrayOfEnums, currentEnum);

            if (index == 0)
                return _arrayOfEnums[_arrayOfEnums.Length - 1];

            return _arrayOfEnums[index - 1];
        }

        private static T[] GetArrayOfEnums()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"{typeof(T)} is not an Enum!");

            return (T[])Enum.GetValues(typeof(T));
        }
    }
}

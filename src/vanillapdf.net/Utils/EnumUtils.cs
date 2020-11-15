using System;

namespace vanillapdf.net.Utils
{
    internal static class EnumUtil<T>
        where T : struct, IConvertible
    {
        static EnumUtil()
        {
            if (!typeof(T).IsEnum) {
                throw new Exception(typeof(T).FullName + " is not an enum type.");
            }
        }

        // Always use checked cast for parsed enum values from the interface
        // If the value could not be interpreted by the enum type
        // it throws exception as it should
        public static T CheckedCast(object enumValue)
        {
            if (!Enum.IsDefined(typeof(T), enumValue))
                throw new InvalidCastException(enumValue + " is not a defined value for enum type " +
                                               typeof(T).FullName);

            return (T)enumValue;
        }

        public static bool IsDefined(T enumValue)
        {
            return Enum.IsDefined(typeof(T), enumValue);
        }
    }

    internal static class EnumExtensions
    {
        public static bool IsDefined<T>(this T enumValue)
            where T : struct, IConvertible
        {
            return EnumUtil<T>.IsDefined(enumValue);
        }
    }
}

using System;

namespace vanillapdf.net.Utils
{
    // Constrained to Enum (not just IConvertible) so the non-boxing Enum.IsDefined<T> overload is
    // available. The constraint is enforced by the compiler, so no runtime type check is needed;
    // avoiding an explicit static constructor also keeps the type beforefieldinit, which spares
    // every static call on this hot path an initialization check.
    internal static class EnumUtil<T>
        where T : struct, Enum
    {
        // Always use checked cast for parsed enum values from the interface
        // If the value could not be interpreted by the enum type
        // it throws exception as it should
        /// <summary>
        /// Cast a value to the target enumeration ensuring it is defined.
        /// </summary>
        /// <param name="enumValue">Value to cast.</param>
        /// <returns>The casted enumeration value.</returns>
        /// <exception cref="InvalidCastException">Thrown when the value is not defined for the enumeration.</exception>
        public static T CheckedCast(object enumValue)
        {
            if (!Enum.IsDefined(typeof(T), enumValue)) {
                throw new InvalidCastException(enumValue + " is not a defined value for enum type " +
                                               typeof(T).FullName);
            }

            return (T)enumValue;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Same validation as <see cref="CheckedCast(object)"/> for a value that already has the
        /// enumeration type — every value the native layer returns through an <c>out</c> parameter.
        /// </summary>
        /// <remarks>
        /// Overload resolution binds enum-typed arguments here. The <c>object</c> overload boxes the
        /// value and validates it through reflection; this one allocates nothing. Interop signatures
        /// therefore declare their <c>out</c> parameter as the enumeration type rather than a raw
        /// integer, so that this overload is the one selected.
        /// </remarks>
        public static T CheckedCast(T enumValue)
        {
            if (!Enum.IsDefined(enumValue)) {
                throw new InvalidCastException(enumValue + " is not a defined value for enum type " +
                                               typeof(T).FullName);
            }

            return enumValue;
        }
#endif

        /// <summary>
        /// Cast a flags value to the target enumeration without requiring it to be a named single value.
        /// Use this for <see cref="System.FlagsAttribute"/> enumerations where combined bit values are valid.
        /// </summary>
        /// <param name="enumValue">Integer value to cast.</param>
        /// <returns>The casted flags enumeration value.</returns>
        public static T FlagsCast(object enumValue)
        {
            return (T)Enum.ToObject(typeof(T), enumValue);
        }

        /// <summary>
        /// Determine whether the specified enumeration value is defined.
        /// </summary>
        /// <param name="enumValue">Enumeration value.</param>
        /// <returns><c>true</c> when the value is defined.</returns>
        public static bool IsDefined(T enumValue)
        {
#if NET5_0_OR_GREATER
            return Enum.IsDefined(enumValue);
#else
            return Enum.IsDefined(typeof(T), enumValue);
#endif
        }
    }

    internal static class EnumExtensions
    {
        /// <summary>
        /// Extension method to check if a value is defined for its enumeration type.
        /// </summary>
        public static bool IsDefined<T>(this T enumValue)
            where T : struct, Enum
        {
            return EnumUtil<T>.IsDefined(enumValue);
        }
    }
}

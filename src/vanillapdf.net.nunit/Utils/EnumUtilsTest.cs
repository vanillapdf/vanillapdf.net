using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit.Utils
{
    /// <summary>
    /// Tests for <see cref="EnumUtil{T}"/>.
    ///
    /// The type has two <c>CheckedCast</c> overloads that must validate identically: the
    /// <c>object</c> overload, which boxes and validates through reflection, and the enum-typed
    /// overload, which does not. Which one a call site binds to depends on the declared type of the
    /// interop <c>out</c> parameter, so the signatures themselves are asserted here too.
    ///
    /// Note: the test project targets net9.0/net10.0 and therefore always exercises the net8.0 build
    /// of the library. The netstandard2.0 build has no enum-typed overload at all and falls back to
    /// the boxing one, so every assertion here holds there as well — nothing in this fixture depends
    /// on which overload is selected, only on the two behaving identically.
    /// </summary>
    [TestFixture]
    public class EnumUtilsTest
    {
        // PdfObjectType runs Undefined = 0 through IndirectReference = 10.
        private const int FirstUndefinedObjectType = 11;

        private static PdfObjectType[] AllObjectTypes()
        {
            return (PdfObjectType[])Enum.GetValues(typeof(PdfObjectType));
        }

        #region CheckedCast validation

        [Test]
        public void CheckedCast_DefinedValue_RoundTrips()
        {
            ClassicAssert.AreEqual(PdfObjectType.Integer, EnumUtil<PdfObjectType>.CheckedCast(PdfObjectType.Integer));
        }

        [Test]
        public void CheckedCast_ZeroIsDefined_RoundTrips()
        {
            ClassicAssert.AreEqual(PdfObjectType.Undefined, EnumUtil<PdfObjectType>.CheckedCast(PdfObjectType.Undefined));
        }

        [Test]
        public void CheckedCast_EveryDeclaredValue_RoundTrips()
        {
            foreach (PdfObjectType value in AllObjectTypes()) {
                ClassicAssert.AreEqual(value, EnumUtil<PdfObjectType>.CheckedCast(value));
            }
        }

        [Test]
        public void CheckedCast_JustPastLastDefinedValue_Throws()
        {
            // The realistic native-drift failure: the native layer gains a value the binding
            // has not been updated for.
            Assert.Throws<InvalidCastException>(
                () => EnumUtil<PdfObjectType>.CheckedCast((PdfObjectType)FirstUndefinedObjectType));
        }

        [Test]
        public void CheckedCast_NegativeValue_Throws()
        {
            Assert.Throws<InvalidCastException>(() => EnumUtil<PdfObjectType>.CheckedCast((PdfObjectType)(-1)));
        }

        [Test]
        public void CheckedCast_MaxValue_Throws()
        {
            Assert.Throws<InvalidCastException>(() => EnumUtil<PdfObjectType>.CheckedCast((PdfObjectType)int.MaxValue));
        }

        [Test]
        public void CheckedCast_ExceptionMessage_NamesTheEnumType()
        {
            var exception = Assert.Throws<InvalidCastException>(
                () => EnumUtil<PdfObjectType>.CheckedCast((PdfObjectType)FirstUndefinedObjectType));

            StringAssert.Contains(typeof(PdfObjectType).FullName, exception.Message);
        }

        #endregion

        #region Overload parity

        [Test]
        public void BothOverloads_AgreeOnEveryDefinedValue()
        {
            foreach (PdfObjectType value in AllObjectTypes()) {
                var viaObject = EnumUtil<PdfObjectType>.CheckedCast((object)value);
                var viaEnum = EnumUtil<PdfObjectType>.CheckedCast(value);

                ClassicAssert.AreEqual(viaObject, viaEnum);
            }
        }

        [TestCase(-1)]
        [TestCase(FirstUndefinedObjectType)]
        [TestCase(int.MaxValue)]
        public void BothOverloads_ThrowTheSameExceptionForUndefinedValues(int raw)
        {
            var value = (PdfObjectType)raw;

            Assert.Throws<InvalidCastException>(() => EnumUtil<PdfObjectType>.CheckedCast((object)value));
            Assert.Throws<InvalidCastException>(() => EnumUtil<PdfObjectType>.CheckedCast(value));
        }

        [Test]
        public void ObjectOverload_AcceptsBoxedUnderlyingInteger()
        {
            // The native layer historically surfaced these values as raw Int32.
            ClassicAssert.AreEqual(PdfObjectType.Integer, EnumUtil<PdfObjectType>.CheckedCast((object)(int)PdfObjectType.Integer));
        }

        #endregion

        #region Flags enumerations

        [Test]
        public void CheckedCast_RejectsCombinedFlagsValue()
        {
            var combined = PdfAnnotationFlags.Hidden | PdfAnnotationFlags.Print;

            Assert.Throws<InvalidCastException>(() => EnumUtil<PdfAnnotationFlags>.CheckedCast(combined));
        }

        [Test]
        public void FlagsCast_AcceptsCombinedFlagsValue()
        {
            var combined = (int)(PdfAnnotationFlags.Hidden | PdfAnnotationFlags.Print);

            ClassicAssert.AreEqual(
                PdfAnnotationFlags.Hidden | PdfAnnotationFlags.Print,
                EnumUtil<PdfAnnotationFlags>.FlagsCast(combined));
        }

        [Test]
        public void FlagsCast_AcceptsUndefinedBits()
        {
            // Flags values are not required to be named, so unknown bits must survive the cast.
            ClassicAssert.AreEqual((PdfAnnotationFlags)4096, EnumUtil<PdfAnnotationFlags>.FlagsCast(4096));
        }

        #endregion

        #region Interop signature guard

        /// <summary>
        /// Native functions whose value is validated through <see cref="EnumUtil{T}.CheckedCast(object)"/>.
        /// Each must declare its <c>out</c> parameter as the enumeration type: declaring it as a raw
        /// integer still compiles and still behaves correctly, but silently rebinds every call site
        /// to the boxing overload.
        /// </summary>
        private static IEnumerable<string> EnumTypedNativeMethods()
        {
            yield return "Object_GetObjectType";
            yield return "StringObject_GetStringType";
            yield return "BaseObjectAttribute_GetAttributeType";
            yield return "XrefEntry_GetType";
            yield return "File_GetVersion";
            yield return "ImageMetadataObjectAttribute_GetColorSpace";
            yield return "ContentInstruction_GetInstructionType";
            yield return "ContentOperator_GetOperatorType";
            yield return "ContentObject_GetObjectType";
            yield return "ContentOperation_GetOperationType";
            yield return "Logging_GetSeverity";
            yield return "TextStringEncoding_Detect";
            yield return "SignatureVerificationResult_GetStatus";
            yield return "Catalog_GetVersion";
            yield return "Catalog_GetPageLayout";
            yield return "ViewerPreferences_GetNonFullScreenPageMode";
            yield return "ViewerPreferences_GetDirection";
            yield return "ViewerPreferences_GetPrintScaling";
            yield return "ViewerPreferences_GetDuplex";
            yield return "Field_GetType";
            yield return "Annotation_GetAnnotationType";
            yield return "Color_GetColorSpace";
            yield return "Date_GetTimezone";
            yield return "Font_GetFontType";
            yield return "CharacterMap_GetCharacterMapType";
            yield return "OutlineBase_GetOutlineType";
            yield return "DocumentSignatureSettings_GetDigest";
            yield return "DocumentEncryptionSettings_GetAlgorithm";
            yield return "Destination_GetDestinationType";
            yield return "Action_GetActionType";
        }

        [TestCaseSource(nameof(EnumTypedNativeMethods))]
        public void NativeMethod_DeclaresEnumTypedOutParameter(string methodName)
        {
            var method = typeof(NativeMethods).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            ClassicAssert.IsNotNull(method, methodName + " was not found on NativeMethods.");

            var outParameter = method.GetParameters().LastOrDefault(p => p.ParameterType.IsByRef);
            ClassicAssert.IsNotNull(outParameter, methodName + " has no by-ref parameter.");

            var elementType = outParameter.ParameterType.GetElementType();
            ClassicAssert.IsTrue(
                elementType.IsEnum,
                methodName + " declares its out parameter as " + elementType.Name +
                "; it must be the enumeration type so that call sites bind to the non-boxing overload.");
        }

        #endregion
    }
}

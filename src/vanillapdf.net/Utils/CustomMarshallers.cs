using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace vanillapdf.net.Utils
{
    internal class EnumMarshaller<T> : ICustomMarshaler where T : struct, IConvertible
    {
        public void CleanUpManagedData(object ManagedObj)
        {
            throw new NotImplementedException();
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            throw new NotImplementedException();
        }

        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            throw new NotImplementedException();
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            var rawValue = Marshal.ReadIntPtr(pNativeData).ToInt64();
            return EnumUtil<T>.CheckedCast(rawValue);
        }
    }

    internal class Utf8StringMarshaler : ICustomMarshaler
    {
        public IntPtr MarshalManagedToNative(object managedObj)
        {
            if (managedObj == null) {
                return IntPtr.Zero;
            }

            if (!(managedObj is string)) {
                throw new MarshalDirectiveException("Utf8StringMarshaler only accepts strings");
            }

            string source = (string)managedObj;
            var utf8Bytes = Encoding.UTF8.GetBytes(source);

            IntPtr pNativeData = Marshal.AllocHGlobal(utf8Bytes.Length + 1);
            Marshal.Copy(utf8Bytes, 0, pNativeData, utf8Bytes.Length);
            Marshal.WriteByte(pNativeData + utf8Bytes.Length, 0);

            return pNativeData;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            List<byte> bytes = new List<byte>();

            while (true) {
                byte current = Marshal.ReadByte(pNativeData);

                // Null terminated strings
                if (current == 0) {
                    break;
                }

                bytes.Add(current);
            }

            var utf8Bytes = bytes.ToArray();
            return Encoding.UTF8.GetString(utf8Bytes);
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            Marshal.FreeHGlobal(pNativeData);
        }

        public void CleanUpManagedData(object managedObj)
        {
        }

        public int GetNativeDataSize()
        {
            return -1;
        }

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return new Utf8StringMarshaler();
        }
    }
}

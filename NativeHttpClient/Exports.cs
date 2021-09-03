using System;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace NativeHttpClient
{
    /**
     * Key limitations:
     * - No dynamic loading (e.g. Assembly.LoadFile)
     * - No runtime code generation (e.g. System.Reflection.Emit)
     * - No C++/CLI, no built-in COM and WinRT interop support
     * - No unconstrained reflection
     * https://github.com/dotnet/runtimelab/blob/feature/NativeAOT/docs/using-nativeaot/limitations.md
     */
    public static class Exports
    {
        /**
         * Exported methods have to be static.
         * Exported methods can only naturally accept or return primitives or value types (i.e structs), they have to marshal all reference type arguments.
         * Exported methods cannot be called from regular managed C# code, an exception will be thrown.
         * Exported methods cannot use regular C# exception handling, they should return error codes instead.
         */
        private static HttpClient client = new HttpClient();

        [UnmanagedCallersOnly(EntryPoint = "http_get")]
        public static IntPtr HttpGet(IntPtr pUrl)
        {
            try
            {
                string url = Marshal.PtrToStringAnsi(pUrl);
                var response = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                return Marshal.StringToHGlobalAnsi(response);
            }
            catch(Exception e)
            {
                return Marshal.StringToHGlobalAnsi(e.Message);
            }
        }
    }
}

# Update 03.2023
Building native libraries that can be consumed by other programming languages with NativeAOT changed since release of .NET 7. For example the ILCompiler NuGet package is no longer needed. Check [this example](https://github.com/dotnet/samples/tree/main/core/nativeaot/NativeLibrary) for more info.

# POC for calling .NET code from C++ using [Native AOT](https://github.com/dotnet/runtimelab/tree/feature/NativeAOT#readme) (.NET 5)
1. Ensure that [pre-requisites](https://github.com/dotnet/runtimelab/blob/feature/NativeAOT/docs/using-nativeaot/prerequisites.md) are installed.
2. Build *NativeHttpClient* project:
   - Static library: `dotnet publish -c Release -r win-x64 /p:NativeLib=Static /p:SelfContained=true`
   - Dynamic library: `dotnet publish -c Release -r win-x64 /p:NativeLib=Shared /p:SelfContained=true`
3. Debug *NativeHttpClient.DynamicLink.App* in x64 architecture.
   - You should be able to debug into `Exports.HttpGet()` C# method.  
4. Disabling a framework features (or enabling a minimal mode of the feature) can result in significant speed and size savings:
   - To remove globalization specific code and data, uncomment a `<InvariantGlobalization>true</InvariantGlobalization>` in the *NativeHttpClient.csproj*. 
   - Uncomment `<IlcDisableReflection>true</IlcDisableReflection>` in the *NativeHttpClient.csproj*.
   - Uncomment `<IlcGenerateStackTraceData>false</IlcGenerateStackTraceData>` in the *NativeHttpClient.csproj*.
   - Uncomment `<IlcOptimizationPreference>Speed</IlcOptimizationPreference>` in the *NativeHttpClient.csproj*.
   - Uncomment `<IlcOptimizationPreference>Size</IlcOptimizationPreference>` in the *NativeHttpClient.csproj*.
   - [More...](https://github.com/dotnet/runtimelab/blob/feature/NativeAOT/docs/using-nativeaot/optimizing.md)

#include <iostream>
#include <cassert>

#ifdef _WIN32
#include "windows.h"
#define symLoad GetProcAddress
#else
#include "dlfcn.h"
#define symLoad dlsym
#endif

typedef const char*(*http_get_proc)(const char*);

int main()
{

#ifdef _WIN32
    auto dllpath = LR"(..\..\NativeHttpClient\bin\Release\net5.0\win-x64\native\NativeHttpClient.dll)";
    HINSTANCE handle = LoadLibrary(dllpath);
#else
    auto dllpath = LR"(./../../NativeHttpClient/bin/Release/net5.0/linux-x64/native/NativeHttpClient.so)";
    void* handle = dlopen(path, RTLD_LAZY);
#endif
    assert(handle != nullptr && "Could not find NativeHttpClient.dll. Wrong DLL location or build architecture mismatch (x64).");

    auto http_get = (http_get_proc)symLoad(handle, "http_get");
    assert(http_get != nullptr && "Could not find http_get() in the exported functions.");

    std::cout << http_get("https://ediscovery.com");
}
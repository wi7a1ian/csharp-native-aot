#include <iostream>

extern "C" const char* http_get(const char* funcName);

int main()
{
    std::cout << http_get("https://ediscovery.com");
}
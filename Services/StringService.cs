using System;

namespace archolosDotNet.Services;

public static class StringService
{
public static string Capitalize(string input)
    {
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}

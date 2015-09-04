using System;
using System.Collections.Generic;

public class Locale
{
    private static Dictionary<string, object> config = ConfigManager.I.GetConfig("Chs");
    public static string Text(int index)
    {
        return Locale.config[index.ToString()] as String;
    }

    public static string Format(int index, params object[] args)
    {
        // TODO: Not implemented yet
        return null;
    }
}
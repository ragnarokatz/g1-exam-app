using System;
using System.Collections.Generic;
using Json = Pathfinding.Serialization.JsonFx;

public class ConfigManager
{
    private static ConfigManager instance = new ConfigManager();
    public static ConfigManager I { get { return ConfigManager.instance; } }

    private Dictionary<string, object> allConfigs;

    private ConfigManager()
    {
        this.allConfigs = new Dictionary<string, object>();
    }

    public void LoadConfig(string name, string json)
    {
        var config = Json.JsonReader.Deserialize<Dictionary<string, object>>(json);
        this.allConfigs.Add(name, config);
    }

    public Dictionary<string, object> GetConfig(string fileName)
    {
        if (! this.allConfigs.ContainsKey(fileName))
        {
            Log.Error("Config with name {0} is not found.", fileName);
            return null;
        }
        return this.allConfigs[fileName] as Dictionary<string, object>;
    }
}

using UnityEngine;

public class Init : MonoBehaviour
{
    private static bool initialized;

    void Awake()
    {
        if (Init.initialized)
            return;

        Init.initialized = true;
        Log.OnLog += HandleOnLog;

        var textAsset = Resources.Load("Configs/Questions", typeof(TextAsset)) as TextAsset;
        ConfigManager.I.LoadConfig("Questions", textAsset.text);

        textAsset = Resources.Load("Configs/Chs", typeof(TextAsset)) as TextAsset;
        ConfigManager.I.LoadConfig("Chs", textAsset.text);
    }

    private void HandleOnLog(Log.LogTypes type, string message)
    {
        switch (type)
        {
        case Log.LogTypes.Trace:
        default:
            Debug.Log(message);
            break;
        case Log.LogTypes.Error:
            Debug.LogError(message);
            break;
        case Log.LogTypes.Warning:
            Debug.LogWarning(message);
            break;
        }
    }

    void OnDestroy()
    {
        Log.OnLog -= HandleOnLog;
    }
}

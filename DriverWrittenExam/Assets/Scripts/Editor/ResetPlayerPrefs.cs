#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class EditorTools
{
    [MenuItem("Edit/Reset Playerprefs")]
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
#endif
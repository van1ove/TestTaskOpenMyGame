using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver
{
    private static int startLevelIndex = -1;
    private static bool anyLevelWasLoaded;

    public static int StartLevelIndex => startLevelIndex;
    public static bool AnyLevelWasLoaded
    {
        get => anyLevelWasLoaded;
        set => anyLevelWasLoaded = value;
    }

    public static bool TryInit(int index)
    {
        if (startLevelIndex != -1) return false;
        
        startLevelIndex = index;
        anyLevelWasLoaded = false;
        return true;
    }

    public static bool IsStartLevel(int index) => startLevelIndex == index;
}

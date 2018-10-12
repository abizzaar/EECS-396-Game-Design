using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Roughly the same thing we did in Homework 2.

public enum PlatformType
{
    Windows,
    Mac,
    Linux
}

public static class Platform {

    public static PlatformType GetPlatform() {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        return PlatformType.Windows;
#elif (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
        return PlatformType.Mac;
#else
        return PlatformType.Linux;
#endif
    }

    private static string GetPlatformString() {
        switch (GetPlatform()) {
            case PlatformType.Windows:
                return "Windows";
            case PlatformType.Mac:
                return "Mac";
            default:
                return "Linux";
        }
    }

    private static string GetAxis(string axisName, int playerNum) {
        // Make our lives easier by using a standard naming convention for the input axes:
        //   [platform][axis]Axis[player]
        return GetPlatformString() + axisName + "Axis" + playerNum.ToString();
    }

    public static string GetMoveAxis(int playerNum) {
        return GetAxis("Move", playerNum);
    }

    public static string GetTurnAxis(int playerNum)
    {
        return GetAxis("Turn", playerNum);
    }
    
    public static string GetFireAxis(int playerNum)
    {
        return GetAxis("Fire", playerNum);
    }
}
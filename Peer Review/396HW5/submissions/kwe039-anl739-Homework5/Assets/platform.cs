using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class platform
{
    private static string _fireAxis = null;

    public static string fireAxis
    {
        get
        {
            if (_fireAxis == null)
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.OSXPlayer:
                    case RuntimePlatform.OSXEditor:
                    case RuntimePlatform.LinuxPlayer:
                    case RuntimePlatform.LinuxEditor:
                        _fireAxis = "FireUnix";
                        break;
                    case RuntimePlatform.WindowsPlayer:
                    case RuntimePlatform.WindowsEditor:
                        _fireAxis = "FireWindows";
                        break;
                    default:
                        return null;
                }
            }
            return _fireAxis; 
        }
    }
}

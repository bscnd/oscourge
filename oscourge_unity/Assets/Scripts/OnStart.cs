using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class OnStart    
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Cursor.visible = false;
        int width = 1280;
        int height = 720;
        bool isFullScreen = false;
        int desiredFPS = 60;

        Screen.SetResolution(width, height, isFullScreen, desiredFPS);
    }
}

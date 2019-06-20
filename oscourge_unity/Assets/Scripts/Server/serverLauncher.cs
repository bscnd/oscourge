using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class serverLauncher : MonoBehaviour
{
   public void Launch()
    {
        Process foo = new Process();
        foo.StartInfo.FileName = Environment.CurrentDirectory + @"\Assets\Scripts\Server\launch.bat";
        foo.StartInfo.Arguments = "1234";
        foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        foo.Start();
        UnityEngine.Debug.Log("Server Launched");
    }

  
}

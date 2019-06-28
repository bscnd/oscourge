using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public  class serverLauncher : MonoBehaviour
{
   public  void Launch(int port)
    {
        try
        {
            UnityEngine.Debug.Log("Server Launched");
            Process foo = new Process();
            foo.StartInfo.FileName = Environment.CurrentDirectory + @"\Assets\Scripts\Server\launch.bat";
            foo.StartInfo.Arguments = port.ToString();
            foo.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            foo.Start();
        }
        catch
        {

        }
    }

  
}

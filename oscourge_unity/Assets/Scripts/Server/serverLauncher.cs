using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
public class serverLauncher : MonoBehaviour
{
    public void Launch(int port)
    {
        try
        {
            UnityEngine.Debug.Log("Server Launched");
            Process foo = new Process();
            if (Application.isEditor)
            {
                foo.StartInfo.FileName = Environment.CurrentDirectory + @"\Assets\Scripts\Server\launch.bat";
            }
            else
            {
                foo.StartInfo.FileName = Application.dataPath + @"\launch.bat";
            }
            foo.StartInfo.Arguments = port.ToString();
            foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; 
            foo.Start();
            StartCoroutine(Exit(foo));
        }
        catch
        {

        }
    }

    IEnumerator
        Exit(Process foo)
    {
        yield return new WaitForSeconds(5);
        if (!foo.HasExited)
        {
            //foo.Kill(); 
        }
    }
}


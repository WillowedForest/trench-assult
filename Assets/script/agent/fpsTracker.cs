using System;
using UnityEngine;
using System.IO;
using Tayx.Graphy;

public class fpsTracker : MonoBehaviour
{
    
    StreamWriter sw;

    void Start()
    {
        sw = new StreamWriter(Application.dataPath + "/mono.cvs");
        sw.WriteLine("Time,AvgFPS,1%low");
    }

    void Update()
    {
        float fps = GraphyManager.Instance.CurrentFPS;
        float avg = GraphyManager.Instance.AverageFPS;
        float low1 = GraphyManager.Instance.OnePercentFPS;
        
        sw.WriteLine($"{Time.time},{fps},{avg},{low1}");
    }
}

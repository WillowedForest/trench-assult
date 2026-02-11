using System;
using UnityEngine;
using System.IO;
using Tayx.Graphy;

public class fpsTracker : MonoBehaviour
{

    // Update is called once per frame
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

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
        
        sw.WriteLine($"{Time.time},{avg},{low1}");
    }
}

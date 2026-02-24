using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager instance;

    private LinkedList<GameObject> agents;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void RegesterAgent(GameObject agent)
    {
        agents.AddLast(agent);
    }

    public List<GameObject> GetAllAgents()
    {
        List<GameObject> agents = new List<GameObject>();  

        foreach (GameObject agent in agents)
        {
            agents.Add(agent);
        }

        return agents;
    }

}

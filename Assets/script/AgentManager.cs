using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager instance;

    private LinkedList<GameObject> agents = new LinkedList<GameObject>();

    private Transform TargetTransform;

    public GameObject player;

    private Queue<Agent> toPathfind = new Queue<Agent>();


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
        List<GameObject> _agents = new List<GameObject>();  

        foreach(GameObject agent in agents)
        {
            _agents.Add(agent);
        }
        return _agents;
    }

    private void FixedUpdate()
    {
        TargetTransform = player.transform;
    }

    public void StartRound()
    {
        foreach(GameObject Agent in agents)
        {
            toPathfind.Enqueue(Agent.GetComponent<Agent>());
        }

        foreach(Agent _agent in toPathfind)
        {
            _agent.StartJob(TargetTransform.position, player);
        }
    }
}
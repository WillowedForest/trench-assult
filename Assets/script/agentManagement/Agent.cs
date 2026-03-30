using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Agent : MonoBehaviour
{
    private ObjectPool<Agent> _pool;

    public NavMeshAgent navMeshAgent;


    public void Init(ObjectPool<Agent> pool)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _pool = pool;
    }

    public void removeFromAgentList()
    {
        AgentManager.instance.UnRegesterAgent(this);
    }

    public void addToAgentList()
    {
        AgentManager.instance.RegesterAgent(this);
    }
}

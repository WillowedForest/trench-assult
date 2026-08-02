using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Agent : MonoBehaviour
{
    private ObjectPool<Agent> _pool;

    public NavMeshAgent navMeshAgent;

    public HurtPlayer HurtPlayer;

    public void Init(ObjectPool<Agent> pool)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _pool = pool;
    }

    public void removeFromAgentList()
    {
        AgentManager.Instance.AddToRemoveQueue(this);
    }

    public void addToAgentList()
    {
        AgentManager.Instance.AddAgentsToAddQueue(this);
    }

    public void Kill()
    {
        _pool.Release(this);
    }
    
}

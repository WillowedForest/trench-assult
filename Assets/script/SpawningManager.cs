using System;
using UnityEngine;
using UnityEngine.Pool;

public class SpawningManager : MonoBehaviour
{

    public static SpawningManager instance;

    public GameObject[] spawnPoints;

    public int spawnCount;

    public int inScene;

    [SerializeField]
    private Agent _agentPrefab;

    public ObjectPool<Agent> Agents;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        if (_agentPrefab == null)
        {
            Debug.Log("forgot to assign the agent prefab please assign it");
            return;
        }
    }

    void Start()
    {

        Agents = new ObjectPool<Agent>
        (
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 100,
            maxSize: 5000
        );
    }

    public ObjectPool<Agent> GetPool()
    {
        return Agents;
    }

    private void OnDestroyItem(Agent bullet)
    {
        Destroy(bullet);
    }

    private void OnRelease(Agent @object)
    {
        @object.navMeshAgent.isStopped = true;
        @object.removeFromAgentList();
        @object.gameObject.SetActive(false);
        inScene--;
    }   

    private void OnGet(Agent @object)
    {
        //Debug.Log($"Get: {@object}");
        @object.gameObject.SetActive(true);
        @object.navMeshAgent.isStopped = false;
        @object.addToAgentList();
        inScene++;
    }

    private Agent CreateItem()
    {
        //Debug.Log($"Create a new object!");
        Agent agent = GameObject.Instantiate(_agentPrefab);
        agent.Init(Agents);
        agent.gameObject.SetActive(false);

        return agent;
    }
    
    public void StartSpawning()
    {
        for (int i = 0; i != spawnCount; ++i)
        {
            GameObject spawner = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Agent agent = Agents.Get();
            agent.transform.position = spawner.transform.position;
        }
    }
}

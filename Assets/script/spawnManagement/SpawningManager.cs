using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawningManager : MonoBehaviour
{

    public static SpawningManager instance;

    public GameObject[] spawnPoints;
    
    private WaitForSeconds _CheckInterval = new WaitForSeconds(0.5f);
    
    //how many need to be spawned at the start of a round
    public int spawnCount = 100;
    
    //how many are still alive in the current level
    public int inScene;

    public bool isInRound = false;

    [SerializeField]
    private Agent _agentPrefab;

    public ObjectPool<Agent> Agents;

    [SerializeField]
    private movement player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public  void Init()
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

    private void OnDestroyItem(Agent Object)
    {
        Destroy(Object);
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
        @object.gameObject.SetActive(true);
        @object.navMeshAgent.isStopped = false;
        @object.addToAgentList();
        inScene++;
    }

    private Agent CreateItem()
    {
        Agent agent = GameObject.Instantiate(_agentPrefab);
        agent.Init(Agents);
        agent.gameObject.SetActive(false);
        return agent;
    }

    public void NextRound()
    {
        int oldSpawnCound = spawnCount;

        if(spawnCount >= 5000)
        {
            Debug.Log(inScene);
            isInRound = true;
            StartCoroutine(ShouldRoundEnd());
        }
        else
        {
            if (oldSpawnCound >= 1000)
            {
                spawnCount = oldSpawnCound + 500;
            }
            else if (oldSpawnCound < 1000)
            {
                spawnCount = oldSpawnCound + 100;
            }
            else
            {
                Debug.LogError("god knows how but there is a valid number in spawn count good luck :3");
            }
        
            for (int i = 0; i != spawnCount; ++i)
            {
                GameObject spawner = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                Agent agent = Agents.Get();
                agent.transform.position = spawner.transform.position;
            }
            Debug.Log(inScene);
            
            AgentManager.instance.StartAgents();
            isInRound = true;
            StartCoroutine(ShouldRoundEnd());
        }
    }

    IEnumerator ShouldRoundEnd()
    {
        while (isInRound)
        {
            if (inScene == 0)
            {
                AgentManager.instance.StopAgents();
                isInRound = false;
                Invoke("NextRound", 1.0f);
                yield return null;
            }
            else
            {
                isInRound = true;
            }

            yield return _CheckInterval;
        }
    }

    
    /// <summary>
    /// only use for agents nothig else it returns the player
    /// </summary>
    public movement getPlayer()
    {
        return player;
    }
}   

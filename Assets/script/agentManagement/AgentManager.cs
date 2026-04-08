using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{
    public static AgentManager instance;

    private List<Agent> agents = new List<Agent>();

    public GameObject player;
    
    private const float DETECTION_RADIUS = 20f;

    private JobHandle handle;

    private bool CalculationCheck = true;
    
    private float3 CachedPlayerPosition;

    private bool upDatePlayerPos = false;
    
    //native arrays
    private NativeArray<float3> agentPositions;
    private NativeArray<bool> agentResults;
    
    private WaitForSeconds recalculatePaths = new WaitForSeconds(0.5f);
    
    private WaitForSeconds _GetPlayerPos = new WaitForSeconds(0.5f);
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void StartAgents()
    {
        CachedPlayerPosition = player.transform.position;
        upDatePlayerPos = true;
        StartCoroutine(GetPlayerPos());

        int agentCount = agents.Count;
        agentPositions = new NativeArray<float3>(agentCount, Allocator.Persistent);
        agentResults = new NativeArray<bool>(agentCount, Allocator.Persistent);
        StartCoroutine(runCalculation());
    }

    public void RegesterAgent(Agent agent)
    {
        StartCoroutine(PathFindFinish(0f));
        agents.Add(agent);
        agentPositions.Dispose();
        agentResults.Dispose();

        int agentCount = agents.Count;
        agentPositions = new NativeArray<float3>(agentCount, Allocator.Persistent);
        agentResults = new NativeArray<bool>(agentCount, Allocator.Persistent);
    }

    public void UnRegesterAgent(Agent agent)
    {
        StartCoroutine(PathFindFinish(0f));
        agents.Remove(agent);
        agentPositions.Dispose();
        agentResults.Dispose();

        int agentCount = agents.Count;
        agentPositions = new NativeArray<float3>(agentCount, Allocator.Persistent);
        agentResults = new NativeArray<bool>(agentCount, Allocator.Persistent);
    }

    public List<Agent> GetAllAgents()
    {
        return agents;
    }
    
    IEnumerator runCalculation()
    {
        while (CalculationCheck)
        {
            PathFind();
            yield return recalculatePaths; 
        }
    }
    
    IEnumerator GetPlayerPos()
    {
        while (upDatePlayerPos)
        {
            CachedPlayerPosition = player.transform.position;
            yield return _GetPlayerPos; 
        }
    }

    public void PathFind()
    {

        for (int i = 0; i == agentPositions.Length - 1; i++)
        {
            agentPositions[i] = agents[i].transform.position;
        }
        
        upDatePlayerPos = false;
       var job = new AgentPlayerCheckJob
       {
           detectionRadius = DETECTION_RADIUS,
           AgentPositions = agentPositions,
           CashedPlayerPosition = player.transform.position,
           Results = agentResults
       };

       handle = job.Schedule(agentPositions.Length, 64);
       StartCoroutine(PathFindFinish(0.15f));
    }

    /// <summary>
    /// never call this on its own its build to be a part of the function with the same name without delayed 
    /// </summary>
    IEnumerator PathFindFinish(float wait)
    {

        yield return new WaitForSeconds(wait);

        handle.Complete();
        StartCoroutine(GetPlayerPos());

        foreach (var result in agentResults)
        {
            int i = 0;
            if (result)
            { 
                agents[i].navMeshAgent.SetDestination(player.transform.position);  
            }
            else
            {
                agents[i].navMeshAgent.SetDestination(CachedPlayerPosition);
            }
            i++;
        }
    }

    private void OnDestroy()
    {
        handle.Complete();
        agentPositions.Dispose();
        agentResults.Dispose();
        agents = null;
        _GetPlayerPos = null;
        recalculatePaths = null;
    }
    
    public void StopAgents()
    {
        CalculationCheck = false;
    }
    
    public void restartAgents()
    {
        CalculationCheck = true;
        StartCoroutine(runCalculation());
    }
    
    /// <summary>
    /// for debug only do not use in final game
    /// </summary>
    public void KillRandomAgent()
    {
        Agent agent = agents[Random.Range(0, agents.Count)];
        SpawningManager.instance.Agents.Release(agent);    
    }

    public void KillAll()
    {
        foreach (Agent agent in agents)
        {
            Destroy(agent);
        }
    }
    
}
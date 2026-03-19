using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager instance;

    private List<Agent> agents = new List<Agent>();



    public GameObject player;
    
    private const float DETECTION_RADIUS = 20f;

    private JobHandle handle;

    private bool CalculationCheck = true;
    
    private float3 CachedPlayerPosition;
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

    void Start()
    {
        CachedPlayerPosition = player.transform.position;
        StartCoroutine(GetPlayerPos());
        
        int agentCount = agents.Count;
        agentPositions = new NativeArray<float3>(agentCount, Allocator.Persistent);
        agentResults = new NativeArray<bool>(agentCount, Allocator.Persistent);
        
        StartCoroutine(runCalculation());
    }
    

    public void RegesterAgent(Agent agent)
    {
        agents.Add(agent);
    }

    public List<Agent> GetAllAgents()
    {
        return agents;
    }
    
    IEnumerator runCalculation()
    {
        while (CalculationCheck)
        {
            StartRound();
            //Debug.Log("yep");
            yield return recalculatePaths; 
        }

    }
    
    IEnumerator GetPlayerPos()
    {
        while (true)
        {
            CachedPlayerPosition = player.transform.position;
            yield return _GetPlayerPos; 
        }

    }
    

    public void StartRound()
    {
        
        for (int i = 0; i < agents.Count; i++)
        {
            agentPositions[i] = agents[i].transform.position;
        }
        
       var job = new AgentPlayerCheckJob
       {
           detectionRadius = DETECTION_RADIUS,
           AgentPositions = agentPositions,
           CashedPlayerPosition = CachedPlayerPosition,
           Results = agentResults
       };
       
       //Debug.Log(CachedPlayerPosition);
       
       handle = job.Schedule(agentPositions.Length, 64);
       handle.Complete();
       
       
       for (int i = 0; i < agents.Count; i++)
       {
           bool targetPosition = agentResults[i];

           if (targetPosition)
           { 
               agents[i].navMeshAgent.SetDestination(player.transform.position);  
           }
           else
           {
               agents[i].navMeshAgent.SetDestination(CachedPlayerPosition);
           }
       }
    }

    private void OnDestroy()
    {
        agentPositions.Dispose();
        agentResults.Dispose();
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
    
    
    
}
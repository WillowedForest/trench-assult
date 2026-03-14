using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager instance;

    private List<GameObject> agents = new List<GameObject>();

    private float3 playerTransform;

    public GameObject player;

    private Queue<Agent> toPathfind = new Queue<Agent>();
    
    private const float DETECTION_RADIUS = 20f;
    
    //native arrays
    private NativeArray<float3> agentPositions;
    private NativeArray<float3> agentCashedPlayerPositions;
    private NativeArray<float3> agentResults;
    
    private WaitForSeconds recalculatePaths = new WaitForSeconds(1);
    
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        int agentCount = 4492;
        agentPositions = new NativeArray<float3>(agentCount, Allocator.Persistent);
        agentCashedPlayerPositions = new NativeArray<float3>(agentCount, Allocator.Persistent);
        agentResults = new NativeArray<float3>(agentCount, Allocator.Persistent);
        
        StartCoroutine(runCalculation());
    }
    

    public void RegesterAgent(GameObject agent)
    {
        agents.Add(agent);
    }

    public List<GameObject> GetAllAgents()
    {
        return agents;
    }

    private void FixedUpdate()
    {
        playerTransform = player.transform.position;
        
    }


    /*IEnumerator runCalculation()
    {
        while (true)
        {
           StartRound();
           yield return recalculatePaths; 
        }
    }*/
    
    IEnumerator runCalculation()
    {
        while (true)
        {
            StartRound();
            Debug.Log("yep");
            yield return recalculatePaths; 
        }

    }

    public void StartRound()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agentCashedPlayerPositions[i] = player.transform.position;
        }
        
       var job = new AgentPlayerCheckJob
       {
           playerPos = playerTransform,
           detectionRadius = DETECTION_RADIUS,
           AgentPositions = agentPositions,
           CashedPlayerPositions = agentCashedPlayerPositions,
           Results = agentResults
       };
       
       JobHandle handle = job.Schedule(agentPositions.Length, 64);
       handle.Complete();

       for (int i = 0; i < agents.Count; i++)
       {
           float3 targetPosition = agentResults[i];
           agents[i].GetComponent<Agent>().navMeshAgent.SetDestination(targetPosition);
       }
    }

    private void OnDestroy()
    {
        agentPositions.Dispose();
        agentCashedPlayerPositions.Dispose();
        agentResults.Dispose();
    }
}
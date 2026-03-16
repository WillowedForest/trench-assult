using System.ComponentModel;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    
    public NavMeshAgent navMeshAgent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        AgentManager.instance.RegesterAgent(this);
    }

}

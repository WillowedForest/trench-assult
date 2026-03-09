using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{

    //a private referance to the navMeshAgant component 
    private NavMeshAgent agent;


    public struct AgentPathfind : IJob
    {
        public Vector3 targetPos;
        public Vector3 currentPos;
        public GameObject _player;
        public NavMeshAgent _agnet;

        public void Execute()
        {
            Vector3 differance = targetPos - currentPos;

            float distance = Mathf.Sqrt(
            Mathf.Pow(differance.x, 2f) +
            Mathf.Pow(differance.y, 2f) +
            Mathf.Pow(differance.z, 2f));

            if (distance <= 10)
            {
                _agnet.SetDestination(_player.transform.position);
            }
            else
            {
                _agnet.SetDestination(targetPos);
            }
        }
    }

    public void StartJob(Vector3 target, GameObject player)
    {
        AgentPathfind JobData = new AgentPathfind
        {
            targetPos = target,
            currentPos = gameObject.transform.position,
            _player = player,
            _agnet = agent
        };

        JobData.Schedule();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AgentManager.instance.RegesterAgent(this.gameObject);
    }
















    // old code that is going to be removed once the refactor and optimisation is complete

    /*void old()
    {
         Vector3 differance = target.transform.position - transform.position;

		float distance = Mathf.Sqrt(
        Mathf.Pow(differance.x, 2f) +
        Mathf.Pow(differance.y, 2f) +
        Mathf.Pow(differance.z, 2f));

        /*if (distance <= 10)
        {
            state = AiStates.chase;
        }
        else
        {
            state = AiStates.idle;
        }

        if (target == null)
            return;

        /*if (distance <= 10)
        {
            state = AiStates.chase;
        }
        else
        {
            state = AiStates.idle;
        }


        switch (state)
        {
            case AiStates.idle:
                break;
            case AiStates.chase:
                agent.SetDestination(target.transform.position);
                break;
        }

    }*/
}

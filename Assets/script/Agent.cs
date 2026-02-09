using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    //this is what the ai will try to get to
    public GameObject target;

    //a private referance to the navMeshAgant component 
    private NavMeshAgent agent;

    private enum AiStates
    {
        idle,
        chase
    }

    private AiStates state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = AiStates.chase;

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;


        /*if (distance <= 10)
        {
            state = AiStates.chase;
        }
        else
        {
            state = AiStates.idle;
        }*/


        switch(state)
        {
            case AiStates.idle:
                break;
            case AiStates.chase:
				agent.SetDestination(target.transform.position);
                break;
		}

    }

    void old()
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
        }*/


    }
}

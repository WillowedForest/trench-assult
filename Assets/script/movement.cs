using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class movement : MonoBehaviour
{

    public float fMoveSpeed = 10.0f;

    private Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // float a = transform.position.y / 2;
		//transform.position = new Vector3(transform.position.x, a, transform.position.z);
    }


    void Update()
    {
        List<GameObject> agents;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
           agents = AgentManager.instance.GetAllAgents();

            foreach (GameObject agent in agents)
            {
                Debug.Log(agent.name);
            }
        }
	}


	void SetTransformX(float n)
	{
		transform.position = new Vector3(n, transform.position.y, transform.position.z);
	}

    void FixedUpdate()
    {
        float fHorizontalInput = Input.GetAxis("Horizontal");

        float fVerticalInput = Input.GetAxis("Vertical");

        Vector3 v3MovementDirection = new Vector3(fHorizontalInput, 0f, fVerticalInput);

        Vector3 movement = v3MovementDirection * fMoveSpeed * Time.deltaTime;

        transform.Translate(movement);


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


}

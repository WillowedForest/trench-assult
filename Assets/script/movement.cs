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
        AgentManager.instance.player = this.gameObject;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            AgentManager.instance.StartRound();
        }
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
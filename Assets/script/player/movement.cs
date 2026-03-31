using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class movement : MonoBehaviour
{

    public float fMoveSpeed = 10.0f;

    public float mouseSensitivity = 2f;

    [SerializeField]
    private Transform cameraTransform;

    //private Rigidbody rb;



    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        //   AgentManager.instance.player = this.gameObject;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawningManager.instance.NextRound();
            Debug.Log(SpawningManager.instance.inScene);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            AgentManager.instance.StopAgents();
        }

        if(Input.GetKeyDown(KeyCode.M)) 
        {
            AgentManager.instance.restartAgents();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AgentManager.instance.KillRandomAgent();
            Debug.Log(SpawningManager.instance.inScene);
        }
	}

    void FixedUpdate()
    {
        float fHorizontalInput = Input.GetAxis("Horizontal");

        float fVerticalInput = Input.GetAxis("Vertical");

        Vector3 v3MovementDirection = new Vector3(fHorizontalInput, 0f, fVerticalInput);

        Vector3 movement = v3MovementDirection * fMoveSpeed * Time.deltaTime;

        transform.Translate(movement);


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        cameraTransform.Rotate(Vector3.left * mouseY);


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

}
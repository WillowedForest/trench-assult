using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class movement : MonoBehaviour
{
    [SerializeField]
    private float fMoveSpeed = 0.1f;
    
    [SerializeField]
    private float mouseSensitivity = 10f;

    [SerializeField]
    private float gravity = -0.5f;

    private CharacterController controller;

    [SerializeField]
    private Transform cameraTransform;

    //private Rigidbody rb;
    [SerializeField]
    private LayerMask _layerMask;

    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        //   AgentManager.instance.player = this.gameObject;
        controller = GetComponent<CharacterController>();
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

        Vector3 v3MovementDirection;




        /*int layerMask = 1 << 7;

        _layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position - new Vector3(0f, -1f, 0f), transform.TransformDirection(Vector3.down), out hit, 2f, _layerMask))
        {
            Debug.DrawRay(transform.position - new Vector3(0f, 1f, 0f), transform.TransformDirection(Vector3.down) * 1f, Color.green);
            transform.Translate(new Vector3(0f, hit.transform.position.y, 0f));
            Debug.Log(hit.distance);
        }
        else
        {
            Debug.DrawRay(transform.position - new Vector3(0f, -1f, 0f), transform.TransformDirection(Vector3.down) * 1000, Color.red);
            transform.Translate(new Vector3(0f, gravity, 0f));
            Debug.Log("Did not Hit");
        }*/

        v3MovementDirection = new Vector3(fHorizontalInput, 0f, fVerticalInput);

        Vector3 movement = transform.right * v3MovementDirection.x + transform.forward * v3MovementDirection.z * fMoveSpeed * Time.deltaTime;


        if (!controller.isGrounded)
         {
            movement.y = gravity;
         }


         controller.Move(movement);



        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        cameraTransform.Rotate(Vector3.left * mouseY);
    }

}
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    private float fMoveSpeed = 0.1f;

    [SerializeField]
    private float gravity = -0.5f;

    [SerializeField] 
    private Transform cameraTransform;

    private CharacterController controller;


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

        v3MovementDirection = new Vector3(fHorizontalInput, 0f, fVerticalInput);

        Vector3 movement = cameraTransform.right * v3MovementDirection.x + cameraTransform.forward * v3MovementDirection.z * fMoveSpeed * Time.deltaTime;
        
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

}

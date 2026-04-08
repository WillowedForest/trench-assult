using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    private float fMoveSpeed = 0.01f;

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

    void FixedUpdate()
    {
        float fHorizontalInput = Input.GetAxis("Horizontal");

        float fVerticalInput = Input.GetAxis("Vertical");

        Vector3 v3MovementDirection;

        v3MovementDirection = new Vector3(fHorizontalInput, 0f, fVerticalInput);

        Vector3 movement = cameraTransform.right * v3MovementDirection.x * fMoveSpeed * Time.deltaTime + cameraTransform.forward * v3MovementDirection.z * fMoveSpeed * Time.deltaTime;

         if (!controller.isGrounded)
         {
             movement.y = gravity;
         }
         else
        {
            movement.y = 0f;
        }
         
        controller.Move(movement);
         
        if (Input.GetKey("escape"))
        {
            SceneLoading.instance.QuitGame();
        }
    }

}

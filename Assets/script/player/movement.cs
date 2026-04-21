using System.Collections;
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
    
    private int health;

    [SerializeField] 
    private int maxHealth = 4;

    [SerializeField] 
    private float healWaitTime = 4;

    [SerializeField] 
    private float healTimer = 1;

    private bool isHealing = false;
    
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        //   AgentManager.instance.player = this.gameObject;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        health = maxHealth;
        SpawningManager.instance.SetPlayer(this);
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
         
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoading.instance.QuitGame();
        }
    }

    public void TakeDamge(int inDamage)
    {
        
        if (isHealing)
        {
            StopCoroutine(WaitToHeal());
            health = health - inDamage;
            StartCoroutine(WaitToHeal());
        }
        else
        {
            isHealing = true;
            health = health - inDamage;
            StartCoroutine(WaitToHeal());
        }
        
        if(health <= 0)
            SceneLoading.instance.LoadMainMenu();
    }

    IEnumerator WaitToHeal()
    {
        yield return new WaitForSeconds(healWaitTime);
        StartCoroutine(Healing());
    }

    IEnumerator Healing()
    {
        while (health != maxHealth)
        {
            health++;
            yield return new WaitForSeconds(healTimer);
        }
    }

    public movement GetPlayer()
    {
        return this;
    }
}

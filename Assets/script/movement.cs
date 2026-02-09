using UnityEngine;

public class movement : MonoBehaviour
{

    public float fMoveSpeed = 10.0f;

    private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // float a = transform.position.y / 2;
		//transform.position = new Vector3(transform.position.x, a, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float fHorizontalInput = Input.GetAxis("Horizontal");

        float fVerticalInput = Input.GetAxis("Vertical");

        Vector3 v3MovementDirection = new Vector3(fHorizontalInput, 0f, fVerticalInput);

        Vector3 movement = v3MovementDirection * fMoveSpeed * Time.deltaTime;

        transform.Translate(movement);

        rb.linearVelocity = movement;

		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}


	void SetTransformX(float n)
	{
		transform.position = new Vector3(n, transform.position.y, transform.position.z);
	}



}

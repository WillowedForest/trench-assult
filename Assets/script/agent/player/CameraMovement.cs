using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public float speedV = 4f;

    public float speedH = 4f;

    private float mouseSensitivity = 10f;

    private float rotationX = 0f;

    private float rotationY = 0f;
    
    void LateUpdate()
    {
        rotationY += speedV = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= speedH = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX = Mathf.Clamp(rotationX, -30f, 30);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}

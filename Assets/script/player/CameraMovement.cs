using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    
    public float speedV = 4f;

    public float speedH = 4f;

    [SerializeField]
    private float mouseSensitivity = 1f;

    private Vector2 Rotation;
    
    [SerializeField]
    private InputActionReference input;
    
    void LateUpdate()
    {
        Rotation.x += input.action.ReadValue<Vector2>().x * mouseSensitivity;
        Rotation.y -= input.action.ReadValue<Vector2>().y * mouseSensitivity;
        
        Rotation.y = Mathf.Clamp(Rotation.y, -30f, 30);

        transform.eulerAngles = new Vector3(Rotation.y, Rotation.x, 0);
    }
}

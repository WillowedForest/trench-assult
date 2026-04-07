using Unity.Mathematics;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, math.INFINITY, layerMask))
            {
                //Debug.Log("aaaaa");
                hit.collider.gameObject.GetComponent<Agent>().Kill();
            }
        }
    }
}

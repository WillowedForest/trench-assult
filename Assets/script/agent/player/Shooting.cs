using Unity.Mathematics;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Score score;

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
                score.AddScore(10);
            }
        }
    }
}

using System.Collections;
using Unity.Mathematics;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Score score;

    private bool canShoot = true;

    private float _shootDelay = 0.4f;

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
            
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, math.INFINITY, layerMask))
            {
                hit.collider.gameObject.GetComponent<Agent>().Kill();
                score.AddScore(10);
            }
            
            
            if (canShoot)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, math.INFINITY, layerMask))
                {
                    hit.collider.gameObject.GetComponent<Agent>().Kill();
                    score.AddScore(10);
                }
                canShoot = false;
                StartCoroutine(ShootDelay());
            }
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_shootDelay);
        canShoot = true;
    }
}

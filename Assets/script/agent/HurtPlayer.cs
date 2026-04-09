using System.Collections;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private WaitForSeconds HitTimer = new WaitForSeconds(0.5f);

    [SerializeField]
    private Collider hurtBox;
    
    private void Start()
    {

    }


    IEnumerator Hitting()
    {
        while (this.gameObject.activeInHierarchy)
        {
            
            yield return HitTimer;
        }
    }
}

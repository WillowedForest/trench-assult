using System.Collections;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private WaitForSeconds HitTimer = new WaitForSeconds(0.5f);

    public movement player;

    private void Start()
    {
       // player = SpawningManager.instance.getPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Hitting());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
            StopCoroutine(Hitting());
    }
    
    IEnumerator Hitting()
    {
        while (this.gameObject.activeInHierarchy)
        {
            player.TakeDamge(1);
            yield return HitTimer;
        }
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Init : MonoBehaviour
{

    private WaitForSeconds RoundStartDelay = new WaitForSeconds(2);
    
    void Start()
    {
        SpawningManager.instance.Init();
        AgentManager.instance.Init();
        StartCoroutine(StartRounds());
    }

    IEnumerator StartRounds()
    {
        yield return RoundStartDelay;
        SpawningManager.instance.NextRound();
    }
}

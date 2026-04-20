using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Init : MonoBehaviour
{

    [SerializeField]
    private movement player;

    private WaitForSeconds RoundStartDelay = new WaitForSeconds(2);
    
    void Start()
    {
        SpawningManager.instance.Init(player);
        AgentManager.instance.Init();
        StartCoroutine(StartRounds());
    }

    IEnumerator StartRounds()
    {
        yield return RoundStartDelay;
        SpawningManager.instance.NextRound();
    }
}

using System.Collections;
using UnityEngine;

public class Init : MonoBehaviour
{

    [SerializeField]
    private movement player;

    private WaitForSeconds _roundStartDelay = new WaitForSeconds(2);
    
    void Start()
    {
        SpawningManager.Instance.Init(player);
        AgentManager.Instance.Init();
        StartCoroutine(StartRounds());
    }

    IEnumerator StartRounds()
    {
        yield return _roundStartDelay;
        SpawningManager.Instance.NextRound();
    }
}

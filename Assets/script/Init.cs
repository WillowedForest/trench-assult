using UnityEngine;

public class Init : MonoBehaviour
{

    public AgentManager _AgentManager;
    public SpawningManager _SpawnManager;
    public GameObject _Player;

    void Start()
    {
        AgentManager.instance.Init();
        SpawningManager.instance.Init();
    }

}

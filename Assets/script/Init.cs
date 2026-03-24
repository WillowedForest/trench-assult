using UnityEngine;

public class Init : MonoBehaviour
{

    public AgentManager _AgentManager;
    public SpawningManager _SpawnManager;
    public GameObject _Player;

    void Awake()
    {
        if(_AgentManager == null)
        {
            Debug.Log("no AgentManager Referance in Init system");
            return;
        }

        if(_SpawnManager == null)
        {
            Debug.Log("no SpawnManager referance in Init system");
            return;
        }

        if( _Player == null)
        {
            Debug.Log("no player reforance in Init");
            return;
        }

        _AgentManager.Init();
        _SpawnManager.Init();
        
    }

}

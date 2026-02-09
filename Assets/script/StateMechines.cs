using UnityEngine;

public class StateMechines : MonoBehaviour
{

    private enum SimpleStates
    {
        StateA,
        StateB,
    }
    
    private SimpleStates state;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        state = SimpleStates.StateB;
        
        switch( state )
        {
            case SimpleStates.StateA:
                Debug.Log("State A");
                break;
            case SimpleStates.StateB:
                Debug.Log("State B");
                break;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerBaseState initialState;
    PlayerBaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionToState(PlayerBaseState newState)
    {
        if (currentState != null)
        {   currentState.OnEnd();   }

        currentState = newState;

        currentState.OnStart();
    }


}

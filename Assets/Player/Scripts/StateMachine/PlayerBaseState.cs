using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : BaseState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerStateFactory states;

    public PlayerBaseState(string stateName, PlayerStateMachine playerStateMachine, PlayerStateFactory playerStateFactory)
    {
        states = playerStateFactory;
        stateMachine = playerStateMachine;
        name = stateName;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    public override void EnterState(StateController player)
    {
        Debug.Log("OOOOOOH That Brothers Falling");
    }

    public override void UpdateState(StateController player)
    {
        Debug.Log("In The Aiiir");
    }

    public override void Move(StateController player, Vector2 inVec)
    {
        throw new System.NotImplementedException();
    }
}

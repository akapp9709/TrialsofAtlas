using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : BaseState
{
    public override void EnterState(StateController player)
    {
        player.PlayerAnimator.SetBool("inCombat", true);
    }

    public override void UpdateState(StateController player)
    {
        //Something will go here I promise
    }

    public override void Move(StateController player, Vector2 inVec)
    {
        throw new System.NotImplementedException();
    }
}

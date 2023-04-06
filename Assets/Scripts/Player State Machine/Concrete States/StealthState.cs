using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthState : BaseState
{
    private bool _isMoving;
    private StateController _player;
    public override void EnterState(StateController player)
    {
        _player = player;
        player.PlayerAnimator.SetBool("isCrouched", true);
        Debug.Log("Dunkin Deez Nuts");
        StateController.crouch += Stand;
    }

    public override void UpdateState(StateController player)
    {
        player.PlayerAnimator.SetBool("isMoving", player.moving);

        if (_isMoving)
        {
            player.PlayerMesh.LookAt(player.PlayerMesh.position + player.PlayerMove.Direction);
        }
    }

    public override void Move(StateController player, Vector2 inVec)
    {
        _isMoving = inVec.magnitude >= 0.5f;
    }

    private void Stand()
    {
        _player.PlayerAnimator.SetBool("isCrouched", false);
        StateController.crouch -= Stand;
        _player.SwitchState(_player.Standard);
    }
}

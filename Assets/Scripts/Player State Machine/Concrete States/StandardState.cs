using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StandardState : BaseState
{
    private bool _test;
    private StateController _player;
    
    public override void EnterState(StateController player)
    {
        _player = player;
        StateController.crouch = Crouch;
    }

    public override void UpdateState(StateController player)
    {
        player.PlayerAnimator.SetBool("isMoving", player.moving);

        //Stand in for strafe animations
        if (_test)
        {
            player.PlayerMesh.LookAt(player.PlayerMesh.position + player.PlayerMove.Direction);
        }
    }

    public override void Move(StateController player, Vector2 inVec)
    {
        _test = inVec.magnitude >= 0.5f;
    }

    private void Crouch()
    {
        StateController.crouch -= Crouch;
        _player.SwitchState(_player.Stealth);
    }
}

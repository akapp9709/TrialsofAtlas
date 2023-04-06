using System.Collections;
using System.Collections.Generic;
using Behavior_Tree;
using UnityEngine;

public class TaskAttack : Node
{
    private Transform _transform, _lastTarget;
    private PlayerManager _playerManager;
    
    public TaskAttack(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform) GetData("target");
        if (target != _lastTarget)
        {
            _playerManager = target.GetComponent<PlayerManager>();
            _lastTarget = target;
        }
        
        /*
         * Handle Damage here
         * Preferably with colliders and animation events tho for more simplicity in this tree.
         * Research enemy damage conductors for inexpensive means of dealing reliable damage.
         * Check for death, add state for player death??
         */

        State = NodeState.Running;
        return State;
    }
}

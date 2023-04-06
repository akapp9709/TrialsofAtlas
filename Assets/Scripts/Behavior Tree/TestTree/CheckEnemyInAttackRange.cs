using System.Collections;
using System.Collections.Generic;
using Behavior_Tree;
using UnityEngine;

public class CheckEnemyInAttackRange : Node
{
    private static int _enemyLayerMask = 1 << 6;
    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        //_animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            State = NodeState.Failure;
            return State;
        }

        Transform target = (Transform) t;
        if (Vector3.Distance(_transform.position, target.position) <= TestTree.attackRange)
        {
            State = NodeState.Success;
            return State;
        }

        State = NodeState.Failure;
        return State;
    }
}

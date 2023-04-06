using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Behavior_Tree;

public class CheckEnemyInFOVRange : Node
{
    private Transform _transform;
    private static int _enemyLayerMask = 1 << 6;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        _enemyLayerMask = LayerMask.GetMask("Player");
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, TestTree.rangeFOV, _enemyLayerMask);
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                Debug.Log($"{this} has succeeded        target is {colliders[0].name}");
                State = NodeState.Success;
                return State;
            }
            
            Debug.Log($"{this} has failed");
            State = NodeState.Failure;
            return State;
        }

        Debug.Log($"{this} has succeeded");
        State = NodeState.Success;
        return State;
    }
}

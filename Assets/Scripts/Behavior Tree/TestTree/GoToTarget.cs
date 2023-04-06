using System.Collections;
using System.Collections.Generic;
using Behavior_Tree;
using UnityEngine;

public class GoToTarget : Node
{
    private Transform _transform;

    public GoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        var target = (Transform) GetData("target");
        
        Debug.Log($"{_transform.name} has seen {target.name}");
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, TestTree.speed);
            _transform.LookAt(target.position);
        }

        State = NodeState.Running;
        return State;
    }
}

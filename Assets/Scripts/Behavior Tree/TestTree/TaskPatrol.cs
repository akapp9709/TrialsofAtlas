using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Behavior_Tree;

// ReSharper disable once CheckNamespace
public class TaskPatrol : Node
{
    private Transform _transform;
    private List<Transform> _waypoints;

    private int _wayPntIndex = 0;
    private float _speed = 2f;

    private float _waitTime = 1f;
    private float _waitCount = 0f;
    private bool _waiting = true;
    
    
    public TaskPatrol(Transform transform, List<Transform> waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCount += Time.deltaTime;
            if (_waitCount >= _waitTime)
                _waiting = false;
        }
        else
        {
            var wp = _waypoints[_wayPntIndex];
            var targetPos = wp.position;
            if (Vector3.Distance(_transform.position, targetPos) < 0.01f)
            {
                _transform.position = targetPos;
                _waitCount = 0f;
                _waiting = true;

                _wayPntIndex = (_wayPntIndex + 1) % _waypoints.Count;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, targetPos, TestTree.speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
        }

        State = NodeState.Running;
        return State;
    }
}

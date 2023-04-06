using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    private Patrol _patrol;
    private NavMeshAgent _agent;
    private List<Transform> _points;
    private TestTree _tree;
    
    public override void EnterState(EnemyStateController enemy)
    {
        Enemy = enemy;
        _agent = enemy.Agent;
        _points = enemy.WayPoints;
        _tree = new TestTree
        {
            waypoints = _points
        };
        _tree.EnterTree(this);
    }

    public override void UpdateState(EnemyStateController enemy)
    {
        _tree.EvaluateTree(this);
    }
}

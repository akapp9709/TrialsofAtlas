using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    private EnemyBaseState _currentState;
    public EnemyIdleState Idle = new EnemyIdleState();
    public EnemyPatrolState Patrol = new EnemyPatrolState();

    [SerializeField] private NavMeshAgent agent;

    [Space][Header("Patrol")] 
    [SerializeField] private List<Transform> waypoints;
    // Start is called before the first frame update
    void Start()
    {
        _currentState = Patrol;
        _currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public NavMeshAgent Agent => agent;
    public List<Transform> WayPoints => waypoints;
}

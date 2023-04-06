using System.Collections.Generic;
using Behavior_Tree;
public class TestTree : Tree
{
    public List<UnityEngine.Transform> waypoints;
    public static float speed = 2f;
    public static float rangeFOV = 5f;
    public static float attackRange = 1f;
    
    protected override Node SetupTree(EnemyBaseState state)
    {
        var tf = state.Controller.transform;
        
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(tf),
                new GoToTarget(tf),
                new CheckEnemyInAttackRange(tf),
                new TaskAttack(tf)
            }),
            new TaskPatrol(tf, waypoints)
        });

        return root;
    }
}

using System.Collections.Generic;

namespace Behavior_Tree
{
    public class Sequence : Node
    {
        public Sequence(): base(){}
        public Sequence(List<Node> children) : base(children) {}
    
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        State = NodeState.Failure;
                        return State;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    case NodeState.Success:
                        continue;
                    default:
                        continue;
                }
            }
            State = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return State;
        }
    }
}

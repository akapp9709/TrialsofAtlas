using UnityEngine;

namespace Behavior_Tree
{
    public abstract class Tree
    {
        private Node _root = null;
        // Start is called before the first frame update
        public void EnterTree(EnemyBaseState state)
        {
            _root = SetupTree(state);
        }

        // Update is called once per frame
        public void EvaluateTree(EnemyBaseState state)
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        protected abstract Node SetupTree(EnemyBaseState state);
    }
}

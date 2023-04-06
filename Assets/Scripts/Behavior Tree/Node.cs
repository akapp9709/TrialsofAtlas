using System.Collections.Generic;
using UnityEngine;

namespace Behavior_Tree
{
    public enum NodeState
    {
        Success,
        Running,
        Failure
    }
    
    public class Node
    {
        protected NodeState State;
        public Node parent = null;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _dataContent = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            Debug.Log($"{children.Count} - number of children");
            foreach (var node in children)
            {
                Attach(node);
            }
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.Failure;

        public void SetData(string name, object content)
        {
            if (!_dataContent.ContainsKey(name))
            {
                _dataContent.Add(name, content);
            }
            
            _dataContent[name] = content;
        }

        public object GetData(string name)
        {
            Debug.Log($"{this} is Trying to get {name}");
            object value = null;
            if (_dataContent.TryGetValue(name, out value))
            {
                return value;
            }
            
            Node node = parent;
            
            while (node != null)
            {
                value = node.GetData(name);
                if (value != null)
                    return value;
                node = node.parent;
            }

            return null;
        }

        public bool ClearData(string name)
        {
            if (_dataContent.ContainsKey(name))
            {
                _dataContent.Remove(name);
                return true;
            }

            var node = parent;
            while (node != null)
            {
                var cleared = node.ClearData(name);
                if (cleared)
                    return true;
                node = node.parent;
            }

            return false;
        }
    }
}

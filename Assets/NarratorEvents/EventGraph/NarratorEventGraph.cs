using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class NarratorEventGraph : NodeGraph {
    private NarratorBaseNode _current;
    public NarratorBaseNode current
    {
        get
        {
            return _current;
        }
        set
        {
            _current = value;
            if (_current is NarratorNode) {
                Debug.Log("New event: " + ((NarratorNode)_current).EventName);
            }
            if (_current != null)
                _current.OnCurrent();
            else {
                Debug.Log("New current node is null, reached end of graph.");
            }
        }
    }
}
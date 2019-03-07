using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(400)]
[NodeTint(0f, 50f, 150f)]
public class NarratorListNode : NarratorBaseNode {

    [Input]
    public Empty Entry;

    [Output(instancePortList = true)]
    public Empty[] Events;

    public override void OnCurrent() {
        List<int> possibleEvents = new List<int>();
        for (int i = 0; i < Events.Length; i++) {
            if (GetOutputPort("Events " + i.ToString()).GetConnections().Count > 0)
                possibleEvents.Add(i);
        }
        int selected = possibleEvents[Random.Range(0, possibleEvents.Count)];
        NarratorBaseNode chosenNode = (NarratorBaseNode)GetOutputPort("Events " + selected.ToString()).Connection.node;
        GetOutputPort("Events " + selected.ToString()).ClearConnections();
        ((NarratorEventGraph)graph).current = chosenNode;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerLocationTrigger : MonoBehaviour
{
    public bool RequireCurrent = true;
    public bool SetToNode;
    public NarratorNode NodeToSet;
    public bool TriggerOnce = true;
    private bool triggered;

    private void OnTriggerEnter(Collider other) {
        if ((TriggerOnce && triggered) || other.tag != "MainCamera") return;
        if (RequireCurrent && ((NarratorNode)NarratorEventManager.Instance.narratorGraph.current).eventObjectTag.Contains(gameObject.tag)) {
            triggered = true;
            if (SetToNode) {
                NarratorEventManager.Instance.SetToNode(NodeToSet);
            } else {
                NarratorEventManager.Instance.PlayerDidAction();
            }
        } else if (!RequireCurrent) {
            triggered = true;
            if (SetToNode) {
                NarratorEventManager.Instance.SetToNode(NodeToSet);
            } else {
                NarratorEventManager.Instance.PlayerDidAction();
            }
        }
        if (TriggerOnce) gameObject.SetActive(false);
    }
}

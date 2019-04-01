using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRTK;
using DG.Tweening;

public class DropLocation : MonoBehaviour
{
    public string objectTagNeeded;
    public bool takeObject;
    public bool floatObject;
    public bool requireDrop;

    private bool triggered;

    private void OnTriggerStay(Collider other) {
        if (!triggered && other.tag == objectTagNeeded) {
            VRTK_InteractableObject iObj = other.GetComponent<VRTK_InteractableObject>();
            if (!requireDrop || !iObj.IsGrabbed()) {
                if (((NarratorNode)NarratorEventManager.Instance.narratorGraph.current).eventObjectTag.Contains(gameObject.tag)) {
                    NarratorEventManager.Instance.PassEvent();
                    if (takeObject) {
                        iObj.ForceStopInteracting();
                    }
                    if (floatObject) {
                        other.transform.DOMove(transform.position, .2f).OnComplete(() => {
                            other.transform.DOMoveY(transform.position.y + .2f, .4f).SetLoops(-1, LoopType.Yoyo);
                        });
                    }
                    iObj.isGrabbable = false;
                    triggered = true;
                }
            }
        }
    }
}

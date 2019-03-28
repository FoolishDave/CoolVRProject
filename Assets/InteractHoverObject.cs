using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VRTK;
using System;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class InteractHoverObject : MonoBehaviour
{
    private VRTK_InteractableObject interactable;
    private bool floating = true;
    private Tweener bob;
    private Tweener spin;
    public float bobAmt = .2f;
    public bool inFrontOfPlayer;

    private void OnEnable() {
        if (!floating) return;
        if (inFrontOfPlayer) {
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * .2f;
        }
        interactable = GetComponent<VRTK_InteractableObject>();
        interactable.InteractableObjectGrabbed += ObjectGrabbed;
        bob = transform.DOMoveY(transform.position.y + bobAmt, 1f).SetLoops(-1, LoopType.Yoyo);
        spin = transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 360), 2f).SetLoops(-1, LoopType.Restart);
    }

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e) {
        floating = false;
        // Let the tween die, kill it if you have to.
        bob.Kill();
        spin.Kill();
    }
}

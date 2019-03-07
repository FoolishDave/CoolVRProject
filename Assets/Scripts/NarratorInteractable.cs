using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NarratorInteractable : MonoBehaviour
{
    public UnityEvent onActivate;
    private bool used = false;

    public void OneTimeActivate() {
        if (!used) {
            Activate();
        }
    }

    private void Activate() {
        used = true;
        onActivate.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorInteractable : MonoBehaviour
{

    private bool used = false;

    public void OneTimeActivate() {
        if (!used) {
            Activate();
        }
    }

    public void Activate() {
        used = true;
        NarratorEventManager.Instance.NextEvent();
    }
}

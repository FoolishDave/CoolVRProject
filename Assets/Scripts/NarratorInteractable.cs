﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void Activate() {
        if (((NarratorNode)NarratorEventManager.Instance.narratorGraph.current).eventObjectTag.Contains(gameObject.tag)) {
            used = true;
            onActivate.Invoke();
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) {
        if (collision.transform.tag != "Donut") return;
        if (((NarratorNode)NarratorEventManager.Instance.narratorGraph.current).SimonSaid) {
            NarratorEventManager.Instance.PassEvent();
        } else {
            NarratorEventManager.Instance.FailEvent();
        }
    }
}

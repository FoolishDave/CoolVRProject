using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

public class NarratorNode : NarratorBaseNode
{

    [Input]
    public Empty previousEvent;

    [Output]
    public Empty pass;

    [Output]
    public Empty fail;

    public string EventName;
    public List<AudioClip> Audio = new List<AudioClip>();
    public string eventObjectTag;
    public bool DisableOnComplete;
    public bool SimonSaid;
    public float EndAfter = 0f;

    private GameObject eventObjects;

    // Use this for initialization
    protected override void Init() {
        
    }

    public void Pass() {
        if (eventObjects)
            eventObjects.SetActive(!DisableOnComplete);
        NarratorEventManager.Instance.StopFailTimer();
        ((NarratorEventGraph)graph).current = (NarratorBaseNode)GetOutputPort("pass").Connection.node;
    }

    public void Fail() {
        if (eventObjects)
            eventObjects.SetActive(!DisableOnComplete);
        NarratorEventManager.Instance.StopFailTimer();
        PunishmentManager.Instance.AngerLevel += 1f;
        ((NarratorEventGraph)graph).current = (NarratorBaseNode)GetOutputPort("fail").Connection.node;
    }

    public override void OnCurrent() {
        if (eventObjectTag != "") {
            eventObjects = Resources.FindObjectsOfTypeAll<Transform>().First(t => t.tag == eventObjectTag).gameObject;
        }
        Debug.Log("Starting node " + EventName);
        if (EndAfter == 0) {
            NarratorEventManager.Instance.StartFailTimer(Audio[0].length);
        } else if (EndAfter > 0) { 
            NarratorEventManager.Instance.StartFailTimer(EndAfter);
        }
        if (eventObjects)
            eventObjects.SetActive(true);
        if (Audio.Count > 0)
            NarratorEventManager.Instance.audioSource.PlayOneShot(Audio[UnityEngine.Random.Range(0,Audio.Count)]);
    }
}

[Serializable]
public class Empty { }
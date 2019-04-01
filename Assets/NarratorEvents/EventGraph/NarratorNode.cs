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
    public string[] eventObjectTag;
    public string[] disableObjects;
    public bool SimonSaid;
    public float EndAfter = 0f;

    private GameObject eventObjects;

    // Use this for initialization
    protected override void Init() {
        
    }

    public void Pass() {
        if (NarratorEventManager.Instance.narratorGraph.current != this) return;
        Debug.Log("Succeeding from node " + EventName);
        Cleanup();
        ((NarratorEventGraph)graph).current = (NarratorBaseNode)GetOutputPort("pass").Connection.node;
    }

    public void Fail() {
        if (NarratorEventManager.Instance.narratorGraph.current != this) return;
        Debug.Log("Failing from node " + EventName);
        Cleanup();
        PunishmentManager.Instance.AngerLevel += 1f;
        ((NarratorEventGraph)graph).current = (NarratorBaseNode)GetOutputPort("fail").Connection.node;
    }

    public void Cleanup() {
        foreach (string obj in disableObjects) {
            Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.tag == obj).ToList().ForEach(o => o.SetActive(false));
        }
        NarratorEventManager.Instance.StopFailTimer();
    }
    
    public override void OnCurrent() {
        foreach (string obj in eventObjectTag) {
            Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.tag == obj).ToList().ForEach(o => o.SetActive(true));
        }
        if (EndAfter > 0) { 
            NarratorEventManager.Instance.StartFailTimer(EndAfter);
        }
        if (Audio.Count > 0) {
            NarratorEventManager.Instance.audioSource.clip = Audio[UnityEngine.Random.Range(0, Audio.Count)];
            if (EndAfter == 0) {
                NarratorEventManager.Instance.StartFailTimer(NarratorEventManager.Instance.audioSource.clip.length);
            }
            NarratorEventManager.Instance.audioSource.time = 0;
            NarratorEventManager.Instance.audioSource.Play();
        }
    }

    public override object GetValue(NodePort port) {
        if (port.IsConnected)
            return port.Connection.node;
        else
            return "Disconnected";
    }

    public IEnumerator failTimer(float time) {
        yield return new WaitForSeconds(time);
        if (SimonSaid)
            Fail();
        else
            Pass();
    }
}

[Serializable]
public class Empty { }
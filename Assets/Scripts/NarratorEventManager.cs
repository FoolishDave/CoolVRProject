using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorEventManager : MonoBehaviour
{
    public static NarratorEventManager Instance;

    public NarratorEventGraph narratorGraph;

    public AudioSource audioSource;
    private Coroutine failTimerRoutine;

    void Start() {
        if (Instance == null) Instance = this;
        else {
            Destroy(this);
            return;
        }

        narratorGraph = (NarratorEventGraph)narratorGraph.Copy();
        narratorGraph.current = (NarratorBaseNode)narratorGraph.nodes[0];
    }

    public void FailEvent() {
        ((NarratorNode)narratorGraph.current).Fail();
    }

    public void PassEvent() {
        ((NarratorNode)narratorGraph.current).Pass();
    }
    
    public void StartFailTimer(float time) {
        failTimerRoutine = StartCoroutine(failTimer(time));
    }

    public void StopFailTimer() {
        StopCoroutine(failTimerRoutine);
    }

    IEnumerator failTimer(float time) {
        yield return new WaitForSeconds(time);
        if (((NarratorNode)narratorGraph.current).SimonSaid)
            FailEvent();
        else
            PassEvent();
    }
}

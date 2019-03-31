using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        failTimerRoutine = null;
    }

    public void SetToNode(NarratorNode node) {
        node.Cleanup();
        narratorGraph.current = node;
    }

    public void PlayerDidAction() {
        if (((NarratorNode)narratorGraph.current).SimonSaid) {
            PassEvent();
        } else {
            FailEvent();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator failTimer(float time) {
        yield return new WaitForSeconds(time);
        if (((NarratorNode)narratorGraph.current).SimonSaid)
            FailEvent();
        else
            PassEvent();
    }
}

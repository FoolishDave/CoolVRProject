using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;

public class NarratorEventManager : MonoBehaviour
{
    public static NarratorEventManager Instance;

    public NarratorEventGraph narratorGraph;
    private NarratorEventGraph baseGraph;

    public AudioSource audioSource;
    private Coroutine failTimerRoutine;

    void Start() {
        if (Instance == null) Instance = this;
        else {
            Destroy(this);
            return;
        }

        baseGraph = narratorGraph;
        narratorGraph = (NarratorEventGraph)baseGraph.Copy();
        narratorGraph.current = (NarratorBaseNode)narratorGraph.nodes[0];
    }

    public void FailEvent() {
        ((NarratorNode)narratorGraph.current).Fail();
    }

    public void PassEvent() {
        ((NarratorNode)narratorGraph.current).Pass();
    }
    
    public void StartFailTimer(float time) {
        failTimerRoutine = StartCoroutine(((NarratorNode)narratorGraph.current).failTimer(time));
    }

    public void StopFailTimer() {
        if (failTimerRoutine != null) {
            StopCoroutine(failTimerRoutine);
            failTimerRoutine = null;
        }
    }

    public void SetToNode(NarratorNode node) {
        node.Cleanup();
        narratorGraph.current = (NarratorNode)narratorGraph.nodes[baseGraph.nodes.IndexOf(node)];
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
}
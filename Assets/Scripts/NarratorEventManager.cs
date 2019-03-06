using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorEventManager : MonoBehaviour
{
    public static NarratorEventManager Instance;

    public List<NarrationObject> NarrationObjects = new List<NarrationObject>();
    public List<GameObject> EventObjects = new List<GameObject>();

    public NarrationObject CurrentEvent
    {
        get
        {
            return NarrationObjects[currentEventIndex];
        }
    }

    public GameObject CurrentObject
    {
        get
        {
            return EventObjects[currentEventIndex];
        }
    }

    public AudioSource audioSource;
    private int currentEventIndex = -1;
    private GameObject eventObject;

    void Start() {
        if (Instance == null) Instance = this;
        else {
            Destroy(this);
            return;
        }

        NextEvent();
    }

    public void NextEvent() {
        if (currentEventIndex >= 0 && CurrentEvent.DisableOnComplete)
            CurrentObject.SetActive(false);
        currentEventIndex++;
        if (currentEventIndex >= NarrationObjects.Count) return;
        if (CurrentEvent.Audio)
            audioSource.PlayOneShot(CurrentEvent.Audio);
        if (CurrentObject) {
            CurrentObject.SetActive(true);
        } else {
            StartCoroutine(WaitForNextEvent(CurrentEvent.WaitUntilNext));
        }
    }

    IEnumerator WaitForNextEvent(float time) {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        yield return new WaitForSeconds(time);
        NextEvent();
    }
}

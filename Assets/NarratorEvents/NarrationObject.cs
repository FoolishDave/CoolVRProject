using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NarratorEvent/Event", order = 1)]
public class NarrationObject : ScriptableObject
{
    public string EventName = "New Event";
    public AudioClip Audio;
    public bool DisableOnComplete;
    public float WaitUntilNext = 0f;
}

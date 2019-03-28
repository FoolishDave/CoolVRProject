using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishmentManager : MonoBehaviour
{
    public static PunishmentManager Instance;

    public AudioSource audioSource;
    public AudioSource globalAudioSource;

    public AudioClip distanceClip;

    public float AngerLevel = 20f;
    private bool distanceTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else {
            Destroy(this);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying && AngerLevel > 20f) {
            if (Random.Range(0f,100f) > 100f - AngerLevel/120f) {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(DOTween.To(() => audioSource.pitch, x => audioSource.pitch = x, Random.Range(.35f, .7f), Random.Range(.05f,.3f)));
                sequence.Append(DOTween.To(() => audioSource.pitch, x => audioSource.pitch = x, 1f, Random.Range(.2f, .4f)));
                sequence.Play();
            }
        }
    }
}

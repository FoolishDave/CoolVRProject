using UnityEngine;

[NodeTint(0f, .9f, .1f)]
public class SavePrefsNode : NarratorBaseNode
{
    [Input]
    public Empty previousEvent;

    [Output]
    public Empty nextEvent;

    public string key;
    public bool value;

    // Use this for initialization
    protected override void Init() {

    }

    public override void OnCurrent() {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        ((NarratorEventGraph)graph).current = (NarratorBaseNode)GetOutputPort("nextEvent").Connection.node;
    }
}

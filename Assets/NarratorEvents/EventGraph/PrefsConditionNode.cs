using UnityEngine;

[NodeTint(0f, .1f, .9f)]
public class PrefsConditionNode : NarratorBaseNode
{
    [Input]
    public Empty previousEvent;

    [Output]
    public Empty trueKey;

    [Output]
    public Empty falseKey;

    public string key;

    // Use this for initialization
    protected override void Init() {

    }

    public override void OnCurrent() {
        if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1) {
            ((NarratorEventGraph)graph).current = (NarratorBaseNode)GetOutputPort("trueKey").Connection.node;
        } else {
            ((NarratorEventGraph)graph).current = (NarratorBaseNode)GetOutputPort("falseKey").Connection.node;
        }
    }
}

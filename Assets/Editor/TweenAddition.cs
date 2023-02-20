using UnityEditor;
using Ui;
using UnityEngine;

[CustomEditor(typeof(TweenAnimation))]
public class TweenAddition : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(20);

        TweenAnimation tweenScript = (TweenAnimation)target;

        GUIStyle style = new()
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            normal = new GUIStyleState() { background = Texture2D.whiteTexture },
            hover = new GUIStyleState() { background = Texture2D.grayTexture },
            active = new GUIStyleState() { background = Texture2D.blackTexture }
        };

        if (GUI.Button(new Rect(20, 40, 200, 20), "START button animations", style))
        {
            tweenScript.Start_Animations();
        }
        GUILayout.Space(10);
        if (GUI.Button(new Rect(20, 70, 200, 20), "STOP button animations", style))
        {
            tweenScript.Stop_All_Animations();
        }
    }
}

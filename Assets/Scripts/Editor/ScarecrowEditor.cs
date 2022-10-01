using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Scarecrow))]
public class ScarecrowEditor : Editor
{
    private void OnSceneGUI()
    {
        Scarecrow scarecrow = (Scarecrow)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(scarecrow.transform.position, Vector3.up, Vector3.forward, 360, scarecrow.AbilityRange);
        Handles.color = Color.white;
        if(scarecrow.HasTarget)
            Handles.DrawLine(scarecrow.transform.position, scarecrow.AbilityTarget.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ParkPlace))]
public class ParkPlaceInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ParkPlace pPlace = (ParkPlace)target;

        if (GUILayout.Button("Generate Random Car"))
        {
            pPlace.GenerateCar();
        }
        if (GUILayout.Button("Clear Place"))
        {
            pPlace.ClearPlace();
        }
    }
}
#endif

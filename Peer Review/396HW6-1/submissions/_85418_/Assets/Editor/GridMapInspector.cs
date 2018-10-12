using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridMap))]
public class GridMapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        GridMap gridMap = (GridMap)target;

        DrawDefaultInspector();
        if (GUILayout.Button("Build Grid"))
        {
            gridMap.BuildGrid();
        }
        if (GUILayout.Button("Clear Grid"))
        {
            gridMap.ClearGrid();
        }
    }
}


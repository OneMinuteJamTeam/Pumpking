using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GrassHeightRandomizerEditor : EditorWindow
{
    [SerializeField] float randomizeHeight;
    [SerializeField] List<GrassRandomizer> grassRandomizers = new List<GrassRandomizer>();

    private SerializedObject serializedObject;
    private SerializedProperty serializedRandomizedHeight;
    private SerializedProperty serializedGrassRandomizers;

    [MenuItem("Tools/Grass height")]
    public static void StartEditor() {
        GrassHeightRandomizerEditor window = EditorWindow.GetWindow<GrassHeightRandomizerEditor>("randomize grass height");
        window.Show();
    }

    private void OnEnable() {
        serializedObject = new SerializedObject(this);
        serializedRandomizedHeight = serializedObject.FindProperty("randomizeHeight");
        serializedGrassRandomizers = serializedObject.FindProperty("grassRandomizers");
    }

    private void randomize() {
        foreach(GrassRandomizer grassRandomizer in grassRandomizers) {
            grassRandomizer.RandomizeHeight(randomizeHeight);
        }
    }

    private void OnGUI() {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space();

        serializedObject.Update();

        GUILayout.Label("randomizeHeight");
        EditorGUILayout.PropertyField(serializedRandomizedHeight,true);

        GUILayout.Label("grass lines");
        EditorGUILayout.PropertyField(serializedGrassRandomizers, true);

        EditorGUILayout.Space();

        if (GUILayout.Button("randomize")) {
            randomize();
        }

        EditorGUILayout.Space();
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

#if UNITY_EDITOR
[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CubeBehavior currentCube = (CubeBehavior)target;
        var buttonColor = currentCube.gameObject.activeSelf ? Color.green : Color.red;
        var cachedColor = GUI.backgroundColor;

        // draws the base inspector GUI for the CubeBehavior component
        base.OnInspectorGUI();
        serializedObject.Update();
        var size = serializedObject.FindProperty("cubeSize");
        EditorGUILayout.PropertyField(size);
        serializedObject.ApplyModifiedProperties();

        if (size.intValue < 1)
        {
            EditorGUILayout.HelpBox("Cube size must be greater than 0! :(", MessageType.Error);
        }
        // Adds buttons in horzizontal layout
        EditorGUILayout.BeginHorizontal();
        // Adds a button to select all cubes in the scene
        if (GUILayout.Button("Select All Cubes"))
        {
            var allCubeBehavior = GameObject.FindObjectsOfType<CubeBehavior>();
            var allCubeGameObjects = allCubeBehavior.Select(cube => cube.gameObject).ToArray();
            Selection.objects = allCubeGameObjects;
        }
        // Adds a button to clear the selection of cubes in the scene   
        if (GUILayout.Button("Clear selection of Cubes"))         {
            Selection.objects = new Object[] { (target as CubeBehavior).gameObject };
        }
        // ends the horizontal layout   
        EditorGUILayout.EndHorizontal();
        // Adds a button to disable or enable all cubes in the scene
        // Cache the current GUI background color
        GUI.backgroundColor = buttonColor;
        if (GUILayout.Button("Disable/Enable all cubes", GUILayout.Height(40)))
        {
            foreach (var cube in GameObject.FindObjectsOfType<CubeBehavior>(true))
            {
                cube.gameObject.SetActive(!cube.gameObject.activeSelf);
                // change cached color based on the active state of the cube    
                if (!cube.gameObject.activeSelf)
                {
                    cachedColor = Color.red;
                }
                else
                {
                    cachedColor = Color.green;
                }
            }
        }
        GUI.backgroundColor = cachedColor;
    }
}
#endif

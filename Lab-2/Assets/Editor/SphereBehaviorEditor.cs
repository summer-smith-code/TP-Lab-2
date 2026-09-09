using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SphereBehavior)), CanEditMultipleObjects]
public class SphereBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // draws the base inspector GUI 
        base.OnInspectorGUI();

        // stores needed variables for determining enable/disable button color
        SphereBehavior currentSphere = (SphereBehavior)target;
        var buttonColor = currentSphere.gameObject.activeSelf ? Color.red : Color.green;
        var originalColor = GUI.backgroundColor;

        // stores variables needed for validating the radius input
        serializedObject.Update();
        var radius = serializedObject.FindProperty("radius");

        // checks to see if radius input is valid 
        if (radius.intValue <= 0)
        {
            EditorGUILayout.HelpBox("The spheres' radius cannot be smaller than 1!", MessageType.Warning);
        }
        
        // adds 2 horizontal adjacent buttons for object selection/deselection
        using (new EditorGUILayout.HorizontalScope())
        {

            // adds a button that selects all spheres in the scene
            if (GUILayout.Button("Select all spheres"))
            {
                var allSphereBehavior = GameObject.FindObjectsOfType<SphereBehavior>();
                var allSphereGameObjects = allSphereBehavior
                                           .Select(sphere => sphere.gameObject)
                                           .ToArray();
                Selection.objects = allSphereGameObjects;
            }

            if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[] { (target as SphereBehavior).gameObject };
            }
        }
        
        // adds a button to disable/enable all spheres in the scene
        GUI.backgroundColor = buttonColor;
        if (GUILayout.Button("Disable/Enable all spheres", GUILayout.Height(40)))
        {
            foreach (var sphere in GameObject.FindObjectsOfType<SphereBehavior>(true))
            {
                sphere.gameObject.SetActive(!sphere.gameObject.activeSelf);
            }
        }
        GUI.backgroundColor = originalColor;
    }
}

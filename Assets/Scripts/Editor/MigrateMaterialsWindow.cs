using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MigrateMaterialsWindow : EditorWindow
{
    [SerializeField]
    private Material _original;
    [SerializeField]
    private Material _replacement;


    [MenuItem("NGL/Migrate Materials Window")]
    private static void ShowWindow() => GetWindow<MigrateMaterialsWindow>().Show();

    private void OnGUI()
    {
        _original = EditorGUILayout.ObjectField(_original, typeof(Material), false) as Material;
        _original = EditorGUILayout.ObjectField(_replacement, typeof(Material), false) as Material;

        using(new EditorGUI.DisabledScope(_original == null || _replacement == null))
        {
            if(GUILayout.Button("Replace all in scene"))
            {
                ReplaceAll();
            }
        }
    }

    private void ReplaceAll()
    {
        GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach(GameObject root in roots)
        {
            foreach(MeshRenderer rend in root.GetComponentsInChildren<MeshRenderer>(true))
            {
                if(rend.sharedMaterial == _original)
                {
                    rend.sharedMaterial = _replacement;
                }
            }
        }
    }
}

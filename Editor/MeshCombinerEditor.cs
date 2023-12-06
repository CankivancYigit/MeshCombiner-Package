#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MeshCombiner meshCombiner = (MeshCombiner)target;

        if (GUILayout.Button("Combine Selected Meshes"))
        {
            meshCombiner.CombineMeshes();
        }
    }
}
#endif


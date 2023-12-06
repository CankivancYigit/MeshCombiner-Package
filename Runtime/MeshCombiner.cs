using UnityEditor;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        CombineMeshes();
    }

    public void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetSelectedMeshFilters();

        if (meshFilters.Length < 2)
        {
            Debug.LogWarning("Not enough selected mesh filters to combine.");
            return;
        }

        // Combine meshes
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        // Create a new GameObject to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.position = Vector3.zero;
        combinedObject.transform.rotation = Quaternion.identity;

        // Add MeshFilter and MeshRenderer components to the new GameObject
        MeshFilter combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer combinedMeshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // Combine meshes into a new mesh
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        // Assign the combined mesh to the MeshFilter
        combinedMeshFilter.sharedMesh = combinedMesh;

        // Set the material of the combined mesh (adjust as needed)
        combinedMeshRenderer.material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        // Optionally, you may want to set other properties of the new GameObject, such as layer, tag, etc.

        // Make the new GameObject active
        combinedObject.SetActive(true);
    }

    MeshFilter[] GetSelectedMeshFilters()
    {
        MeshFilter[] meshFilters = new MeshFilter[0];

        // Get selected objects
        GameObject[] selectedObjects = Selection.gameObjects;

        // Filter MeshFilter components
        foreach (GameObject obj in selectedObjects)
        {
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                System.Array.Resize(ref meshFilters, meshFilters.Length + 1);
                meshFilters[meshFilters.Length - 1] = meshFilter;
            }
        }

        return meshFilters;
    }
}


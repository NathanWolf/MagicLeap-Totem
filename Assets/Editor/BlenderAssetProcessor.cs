using UnityEngine;
using UnityEditor;
using System.IO;
 
// From: https://forum.unity.com/threads/handling-unity-blender-coordinate-system-differences.449581/
// Fixes up the Blender coordinate system
public class BlenderAssetProcessor : AssetPostprocessor
{
 
    public void OnPostprocessModel(GameObject obj)
    {
        //only perform corrections with blender files
        ModelImporter importer = assetImporter as ModelImporter;
        if (Path.GetExtension(importer.assetPath) == ".blend")
        {
            RotateObject(obj.transform, Quaternion.identity);
        }
    }
 
    //recursively rotate a object tree individualy
    private void RotateObject(Transform obj, Quaternion parentRotation)
    {
        //if a meshFilter is attached, we rotate the vertex mesh data
        MeshFilter meshFilter = obj.GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (meshFilter)
        {
            RotateMesh(meshFilter.sharedMesh);
        }

        Quaternion thisRotation = obj.transform.rotation;
        Quaternion translated = thisRotation;// * parentRotation;
        //translated = Quaternion.Euler(translated.eulerAngles.x,
        //    translated.eulerAngles.z, translated.eulerAngles.y);

        parentRotation = thisRotation;
        
        translated = Quaternion.identity *
                                 Quaternion.AngleAxis(translated.eulerAngles.x, Vector3.right) *
                                 Quaternion.AngleAxis(translated.eulerAngles.y, Vector3.forward) *
                                 Quaternion.AngleAxis(-translated.eulerAngles.z, Vector3.up);

        // obj.transform.rotation = translated * Quaternion.Inverse(parentRotation);
        obj.transform.rotation = translated * parentRotation;

        // obj.transform.rotation = Quaternion.identity;
        
        //do this too for all our children
        //Casting is done to get rid of implicit downcast errors
        foreach (Transform child in obj)
        {
            AdjustOffset(child);
            RotateObject(child, parentRotation);
        }
    }
 
    //"rotate" the mesh data
    private void RotateMesh(Mesh mesh)
    {
        int index = 0;
 
        //switch all vertex z values with y values
        Vector3[] vertices = mesh.vertices;
        for (index = 0; index < vertices.Length; index++)
        {
            vertices[index] = new Vector3(-vertices[index].x, vertices[index].z, vertices[index].y);
        }
        mesh.vertices = vertices;
 
        //recalculate other relevant mesh data
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
 
    private void AdjustOffset(Transform obj)
    {
        // adjust the local position to take into account the switching of the mesh axis
        obj.localPosition = new Vector3(-obj.localPosition.x, obj.localPosition.z, obj.localPosition.y);
    }
}
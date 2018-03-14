using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour {

    public Material MaterialGPURows;
    public Material MaterialGPURows1;
    public Material Materialboat;
    public Material MaterialLine;
    public Material MaterialSelectLine;

    private void Awake()
    {
        GlobalVariableBackground.Instance.modelPerfab = Resources.Load("CubePerfab") as GameObject;
        GlobalVariableBackground.Instance.waterPerfab = Resources.Load("WaterPerfab") as GameObject;
        GlobalVariableBackground.Instance.pointPerfab = Resources.Load("SpherePerfab") as GameObject;
        GlobalVariableBackground.Instance.ArrowMesh = Resources.Load("jiantou") as Mesh;
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Mesh mesh = go.GetComponent<MeshFilter>().mesh;
        List<Vector3> listvec = new List<Vector3>();
        float w = GlobalVariableBackground.Instance.pointWigth;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            listvec.Add(new Vector3(mesh.vertices[i].x * w, mesh.vertices[i].y * w, mesh.vertices[i].z * w));
        }
        Mesh newMesh = new Mesh();
        newMesh.vertices = listvec.ToArray();
        //newMesh.vertices = mesh.vertices;
        newMesh.triangles = mesh.triangles;
        newMesh.normals = mesh.normals;
        GlobalVariableBackground.Instance.miniCubeMesh = newMesh;
        Destroy(go);

        GlobalVariableBackground.Instance.MaterialGPURows = MaterialGPURows;
        GlobalVariableBackground.Instance.MaterialGPURows1 = MaterialGPURows1;
        GlobalVariableBackground.Instance.Materialboat = Materialboat;
        GlobalVariableBackground.Instance.MaterialLine = MaterialLine;
        GlobalVariableBackground.Instance.MaterialSelectLine = MaterialSelectLine;


    }
}

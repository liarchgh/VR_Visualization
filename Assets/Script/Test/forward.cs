using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forward : MonoBehaviour {

    // Use this for initialization
    public GameObject p1;
   // public GameObject p2;

    void Start () {
        //Quaternion LookRotation(Vector3 forward, Vector3 upwards);
        //CombineMesh();
        ss();
    }

    private void Update()
    {
        //transform.LookAt(p2.transform);
        //transform.rotation =
        //Quaternion.LookRotation(p2.transform.position, Vector3.forward);
    }


    public void CombineMesh()
    {
        //CombineInstance[] combine = new CombineInstance[2];
        //combine[0].mesh = p1.GetComponent<MeshFilter>().mesh;
        //combine[1].mesh = p2.GetComponent<MeshFilter>().mesh;
        //transform.GetComponent<MeshFilter>().mesh = new Mesh();
        //transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine,true);
        //UnityEditor.AssetDatabase.CreateAsset(transform.GetComponent<MeshFilter>().mesh, "Assets/jiantou.asset");
    }

    public void ss()
    {
        Mesh m = GetComponent<MeshFilter>().mesh;
        Vector3[] vecnormals = m.normals;
        Vector3[] vecVec = m.vertices;
        for (int i = 0; i < vecnormals.Length; i++)
        {
            GameObject.Instantiate(p1, this.transform.TransformPoint(vecVec[i]), Quaternion.LookRotation(vecnormals[i]));
        }

    }

}

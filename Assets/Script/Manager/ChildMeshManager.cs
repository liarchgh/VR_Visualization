using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMeshManager : MonoBehaviour {

    public Others.ModelState modelState;
    private Mesh originalMesh;
    private Mesh symmetricMesh;
    // Use this for initialization
    void Start () {
        originalMesh = GetComponent<MeshFilter>().mesh;

    }

    public void stateUpdate()
    {
        modelStateUpdate();
        EnableChange();
    }

    public void EnableChange()
    {
        if (transform.childCount > 0)
        {
            GameObject child = transform.GetChild(0).gameObject;

            child.GetComponent<MeshCollider>().enabled = GetComponent<MeshCollider>().enabled;
            child.GetComponent<MeshRenderer>().enabled = GetComponent<MeshRenderer>().enabled;
        }
    }

    public void modelStateUpdate()
    {
        GameObject syGO;
        symmetricManager symanager;

        if (transform.childCount == 0)
        {
            syGO = GameObject.Instantiate(GlobalVariableBackground.Instance.modelPerfab);
            syGO.transform.parent = transform;
            symanager =syGO.AddComponent<symmetricManager>();
        }
        else
        {
            syGO = transform.GetChild(0).gameObject;
            symanager = syGO.GetComponent<symmetricManager>();
        }

        if (syGO.name.Contains(name)&& symanager.symode==modelState.symmetricmode)
        {
            return;
        }

        syGO.name = name + "*symmetric";
        symanager.symode = modelState.symmetricmode;

        symmetricMesh = syGO.GetComponent<MeshFilter>().mesh;

        Vector3[] vecParentArray = originalMesh.vertices;
        int[] triParentArray = originalMesh.triangles;

        Vector3[] vecNewArray = new Vector3[vecParentArray.Length];
        int[] triNewArray = new int[triParentArray.Length];

        for (int i = 0; i < vecNewArray.Length; i++)
        {
            Vector3 v = transform.TransformPoint(vecParentArray[i]);
            switch (modelState.symmetricmode)
            {
                case Enums.SymmetricMode.NONE:
                    break;
                case Enums.SymmetricMode.X:
                    vecNewArray[i] = new Vector3(-v.x, v.y, v.z);
                    break;
                case Enums.SymmetricMode.Y:
                    vecNewArray[i] = new Vector3(v.x, -v.y, v.z);
                    break;
                case Enums.SymmetricMode.Z:
                    vecNewArray[i] = new Vector3(v.x, v.y, -v.z);
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < triNewArray.Length; i++)
        {
            switch (i % 3)
            {
                case 0:
                    triNewArray[i] = triParentArray[i + 2];
                    break;
                case 1:
                    triNewArray[i] = triParentArray[i];
                    break;
                case 2:
                    triNewArray[i] = triParentArray[i - 2];
                    break;
                default:
                    break;
            }
        }
        int length = symmetricMesh.vertices.Length;
        if (length > vecNewArray.Length)
        {
            symmetricMesh.triangles = triNewArray;
            symmetricMesh.vertices = vecNewArray;
        }
        else
        {
            symmetricMesh.vertices = vecNewArray;
            symmetricMesh.triangles = triNewArray;
        }
        symmetricMesh.colors = originalMesh.colors;
        symmetricMesh.RecalculateNormals();

    }


}

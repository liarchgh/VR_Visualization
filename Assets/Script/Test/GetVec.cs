using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVec : MonoBehaviour {

    //public Vector3[] vec;
    public GameObject go;
    public Plane p;
    private Matrix4x4 m;
	// Use this for initialization
	void Start () {
        p = new Plane();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m = go.transform.localToWorldMatrix;
           p = m.TransformPlane(p);
        }
	}

}

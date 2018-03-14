using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalVariableBackground.Instance.CutKnife = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

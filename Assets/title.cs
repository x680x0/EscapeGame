using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("z")) {
            Application.LoadLevel("main");
        }
	}
}

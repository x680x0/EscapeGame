using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {

    public Image gage;
    public GameObject player;
    objectBase objec;
	// Use this for initialization
	void Start () {
        objec = player.GetComponent<objectBase>();
	}
	
	// Update is called once per frame
	void Update () {
        float tmp = (float)objec.HP / (float)objec.MAXHP;
        gage.fillAmount = tmp;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer:MonoBehaviour {

    public Image gage;
    public GameObject player;
    lightScript objec;
    // Use this for initialization
    void Start() {
        objec = player.GetComponent<lightScript>();
    }

    // Update is called once per frame
    void Update() {
        float tmp = (float)objec.time / (float)20;
        gage.fillAmount = tmp;
    }
}

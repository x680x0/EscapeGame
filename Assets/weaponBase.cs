using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBase : MonoBehaviour {
    Animator animator;
    static int[] blow;
    static int none;
    public GameObject[] attackpivot;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        blow = new int[4];
        blow[0] = Animator.StringToHash("up");
        blow[1] = Animator.StringToHash("right");
        blow[2] = Animator.StringToHash("down");
        blow[3] = Animator.StringToHash("left");
        none = Animator.StringToHash("none");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void PlayAnimation(int vector) {
        animator.Play(blow[vector]);
    }
    public void StopAnimation() {
        animator.Play(none);
    }
}

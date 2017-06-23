using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBase : objectBase {

    public string label;
    [System.NonSerialized]
    public int power;
    public bool grounded;
    [System.NonSerialized]
    public GameObject actor;
    [System.NonSerialized]
    public Animator animator;
    // Use this for initialization
    public override void Start() {
        base.Start();
        animator = GetComponent<Animator>();
      //  Through(0);
    }
    public override void Update() {
        base.Update();
    }
    public override void FixedUpdate() {
        inLight = false;
        Collider2D[][] groundCheckCollider = new Collider2D[1][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(pivot[vector].transform.position);
        foreach(Collider2D[] groundCheckList in groundCheckCollider) {
            foreach(Collider2D groundCheck in groundCheckList) {
                if(groundCheck != null) {
                    if(!groundCheck.isTrigger) {
                        if(groundCheck.tag == "Light") {
                            inLight = true;
                        }
                        if(!grounded&&groundCheck.tag == "Wall"&& groundCheck.gameObject.transform.parent.gameObject != actor) {
                            grounded = true;
                          //  groundCheck.gameObject.transform.parent.gameObject.GetComponent<objectBase>().Damaged(1, objectBase.typeOfDamage.cross);
                        }
                    }
                }

            }
        }
        if(!grounded) {
            readyX = 0; readyY = 0;//初期化
            SetVector(vector, power);
            Move();
        }
    }
    public virtual void Through(int _vector,GameObject _actor,int _power,int _X,int _Y) {
        MGR = GameObject.Find("mgrObject").GetComponent<mgrScript>();
        once = MGR.once;
        SetPosition(_X, _Y);
        power = _power;
        actor=_actor;
        grounded = false;
        vector = _vector;
        animator = GetComponent<Animator>();
        animator.Play("through");
    }
    public virtual void Grounded() {
        grounded = true;

        animator.Play("stop");
    }
    public virtual void SetGrounded() {
        grounded = true;
        actor = null;
    }
}

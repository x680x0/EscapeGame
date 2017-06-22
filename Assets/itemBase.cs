using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBase : objectBase {
    public bool grounded;
    Animator animator;
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
        base.FixedUpdate();
        if(!grounded) {
            readyX = 0; readyY = 0;//初期化
            SetVector(vector, 30);
            Move();
        }
    }
    public virtual void Through(int _vector) {
        grounded = false;
        vector = _vector;
        animator.Play("through");
    }
    public virtual void Grounded() {
        grounded = true;

        animator.Play("stop");
    }
}

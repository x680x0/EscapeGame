using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tossin:EnemyScript {

    static int[] attack, Go, End, walk;

    int Phase;
    float Timer,Sp;
    Animator animator;

    public override void Start() {
        Sp = 10;
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Phase = 0;
        attack = new int[4];
        attack[0] = Animator.StringToHash("attackup");
        attack[1] = Animator.StringToHash("attackright");
        attack[2] = Animator.StringToHash("attackdown");
        attack[3] = Animator.StringToHash("attackleft");

        End = new int[4];
        End[0] = Animator.StringToHash("Endup");
        End[1] = Animator.StringToHash("Endright");
        End[2] = Animator.StringToHash("Enddown");
        End[3] = Animator.StringToHash("Endleft");

        Go = new int[4];
        Go[0] = Animator.StringToHash("Goup");
        Go[1] = Animator.StringToHash("Goright");
        Go[2] = Animator.StringToHash("Godown");
        Go[3] = Animator.StringToHash("Goleft");


        walk = new int[4];
        walk[0] = Animator.StringToHash("up");
        walk[1] = Animator.StringToHash("right");
        walk[2] = Animator.StringToHash("down");
        walk[3] = Animator.StringToHash("left");
    }
    // Update is called once per frame
    public override void FixedUpdate() {
        if(HP > 0) {
            switch(Phase) {
                case 0:
                    Timer = 10;
                    Phase = 1;
                    muki = Random.Range(0,4);
                    animator.Play(walk[muki]);
                    switch(muki) {
                        case 0:
                            rb2d.velocity = Vector2.up * Sp;
                            break;
                        case 1:
                            rb2d.velocity = Vector2.right * Sp;
                            break;
                        case 2:
                            rb2d.velocity = Vector2.down * Sp;
                            break;
                        case 3:
                            rb2d.velocity = Vector2.up * Sp;
                            break;
                    }
                    break;
                case 1:
                    if(Timer > 0) {
                        Timer -= 1f;
                        if(Timer <= 0) {
                            Timer = 0;
                            Phase = 0;
                        }
                    }
                    break;
            }
        } else {

        }

    }
}

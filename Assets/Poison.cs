using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison:EnemyScript {

    static int walk;
    static int Bomb;

    int Phase;
    float Timer, Sp;
    Animator animator;
    public GameObject Fog;
    public override void Start() {

        base.Start();
        Timer = 30;
        bc2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        ReTarget();
        animator = GetComponent<Animator>();
        walk = Animator.StringToHash("walk");
        Bomb = Animator.StringToHash("Bomb");
        animator.Play(walk);
    }
    public GameObject[] attackCol;
    bool AttackNow;
    float Attacktime = 0;
    BoxCollider2D bc2d;
    public int SpeedRange1, SpeedRange2;
    // Use this for initialization
    
    public override void FixedUpdate() {
        int i = 0;
        Timer -= 0.1f;
        if(Timer < 0) {
            ShelfBomb();
        }
        if(HP > 0) {
            if(Target == null) {
                ReTarget();
            }
            Vector2 walkvect = new Vector2(0, 0);
            if(AttackNow) {
                rb2d.velocity = Vector2.zero;
                rb2d.mass = 10;

            } else {
                Attacktime -= 0.1f;
                if(HP > 0) {
                    if(Target != null) {
                        walkvect = Target.GetPos() - (Vector2)transform.position;
                        walkvect.Normalize();
                        walkvect *= Random.Range((float)SpeedRange1, (float)(SpeedRange2));
                    }
                }
                if(Random.Range(0, 10) < 5) {
                    walkvect.Set(Random.Range(-4, 4), Random.Range(-4, 4));
                }
                rb2d.velocity = walkvect;
                SetOrder(0);
                
            }
            Collider2D[][] CheckCollider = new Collider2D[1][];
            CheckCollider[0] = Physics2D.OverlapPointAll(pivot[muki].transform.position);
            inLight = false;
            foreach(Collider2D[] CheckList in CheckCollider) {

                foreach(Collider2D groundCheck in CheckList) {
                    if(groundCheck != null) {
                        if(groundCheck.isTrigger) {
                            if(groundCheck.tag == "PlayerAttack") {
                                int c = groundCheck.gameObject.transform.parent.gameObject.GetComponent<CNum>().GetContlol();
                                Damaged(groundCheck.gameObject, c);
                            }
                            if(groundCheck.tag == "PlayArea") {
                                inLight = true;
                            }
                        }
                    }
                }

            }
        }
    }

    public override void Damaged(GameObject obj, int num) {
        Instantiate(DamageParticle, transform);
        ShelfBomb();
    }
    public void ShelfBomb() {
        HP = 0;
        rb2d.velocity = Vector2.zero;
        animator.Play("Bomb");
    }
    public void FogIns() {
        Instantiate(Fog,transform.position,transform.localRotation);
        Destroy(gameObject);
    }
}

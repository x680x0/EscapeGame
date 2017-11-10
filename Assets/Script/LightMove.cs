using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove:MonoBehaviour {
    public float speed;
    Rigidbody2D rb2d;

    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
    }
    void FixedUpdate() {
        Collider2D[][] CheckCollider = new Collider2D[1][];
        CheckCollider[0] = Physics2D.OverlapPointAll(transform.position);

        foreach(Collider2D[] CheckList in CheckCollider) {

            foreach(Collider2D groundCheck in CheckList) {
                if(groundCheck != null) {
                    if(groundCheck.isTrigger) {
                        if(groundCheck.tag == "LightStop") {
                            rb2d.velocity = groundCheck.gameObject.GetComponent<LitghtPoint>().ToNext()*speed;
                        }
                    }
                }
            }

        }
    }
}
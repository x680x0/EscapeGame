using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Objects {

    public float nspeed;
    public override void FixedUpdate() {
    }
    public void ini(int vect) {
        rb2d = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 0, -90 * (vect-1));
        switch(vect) {
            case 0:
                rb2d.velocity = Vector2.up * nspeed;
                break;
            case 1:
                rb2d.velocity = Vector2.right * nspeed;
                break;
            case 2:
                rb2d.velocity = Vector2.down * nspeed;
                break;
            case 3:
                rb2d.velocity = Vector2.left * nspeed;
                break;

        }
    }
}

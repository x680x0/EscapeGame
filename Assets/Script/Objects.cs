using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour {

    public GameObject[] pivot;
    public bool inLight;
    protected int muki;
    protected bool move;


    protected Rigidbody2D rb2d;
    protected Vector2 speed;
    protected SpriteRenderer sr;
    public float ss;

    // Use this for initialization
    virtual public void Start () {

        move = false;
        muki = 2;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        speed = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    virtual public void FixedUpdate() {
        SetOrder(20);
    }
    public void SetOrder(int dif) {
        if(sr != null) {
            sr.sortingOrder = (int)(transform.position.y * -100) + dif;
        }
    }
    public Vector2 GetPos() {
        return transform.position;
    }
    public int Vector2int(Vector2 vect) {
        vect.Normalize();
        float angle= Mathf.Rad2Deg * Mathf.Atan2(vect.y, vect.x);
        if(0 <= angle && angle <= 45) {
            return 1;
        } else if(45 <= angle && angle <= 135) {
            return 0;
        } else if(135 <= angle && angle <= 180) {
            return 3;
        } else if(0 >= angle && angle >= -45) {
            return 1;
        } else if(-45 >= angle && angle >= -135) {
            return 2;
        } else if(-135 >= angle && angle >= -180) {
            return 3;
        }
        return -1;
    }

}

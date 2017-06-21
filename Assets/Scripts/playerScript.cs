using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript:objectBase {
    public int x, y;
    SpriteRenderer spriteRenderer;
    public Sprite[] arrow;
    // Use this for initialization
    public override void Start() {
        base.Start();
        x = 0;
        y = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        vector = 0;
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
    }
    public override void FixedUpdate() {
        base.FixedUpdate();

        readyX = 0; readyY = 0;//初期化

        if(MGR.input[vector] > 0) {
            stop = false;
            SetVector(vector);
            if(MGR.input[(vector + 1) % 4] > 0) {
                SetVector((vector + 1) % 4);
            }
            if(MGR.input[(vector + 3) % 4] > 0) {
                SetVector((vector + 3) % 4);
            }
        } else {
            stop = true;
            for(int i = 0; i < 4; i++) {
                if(MGR.input[i] > 0) {
                    vector = i;
                    stop = false;
                }
            }
            spriteRenderer.sprite = arrow[vector];
        }
        Move();
    }
}

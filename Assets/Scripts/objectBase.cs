using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBase:MonoBehaviour {
    [System.NonSerialized]
    public SpriteRenderer spriteRenderer;
    // [System.NonSerialized]
    public float once;
    public int X, Y;
    public int MAXHP, HP;
    [System.NonSerialized]
    public int readyX, readyY;
    public GameObject[] pivot;//光に入っているか判定する座標
    [System.NonSerialized]
    public mgrScript MGR;//入力や全体を通しての初期値などはここから
    public bool inLight;//光の当たる範囲
    public int vector;//上から時計回りに0123
    public bool stop;
    public enum typeOfDamage {
        cross = 0,
        mid,
        slip
    }
    // Use this for initialization
    public virtual void Start() {
        inLight = false;
        MGR = GameObject.Find("mgrObject").GetComponent<mgrScript>();
        stop = true;
        once = MGR.once;
        spriteRenderer = GetComponent<SpriteRenderer>();
        X = (int)(transform.localPosition.x / once);
        Y = (int)(transform.localPosition.y / once);
    }

    // Update is called once per frame
    public virtual void Update() {
        spriteRenderer.sortingOrder = -Y;
        transform.localPosition = new Vector3(X * once, Y * once, transform.localPosition.z);
    }
    public virtual void FixedUpdate() {

    }

    public virtual void Damaged(int damage, typeOfDamage type, int _X, int _Y,GameObject Attacker) {

      
    } 

    public virtual void Move() {//実際に動かす　SetMoveで事前にセットしておくこと
        if(readyX * readyY == 0) {
            X += readyX;
            Y += readyY;
        } else {
            X += (int)(readyX / 1.4);
            Y += (int)(readyY / 1.4);
        }
    }
    public virtual void SetVector(int _vector, int speed) {//これでSetしてMoveで実際に動かす。
        switch(_vector) {
            case 0:
                readyY += speed;
                break;
            case 1:
                readyX += speed;
                break;
            case 2:
                readyY -= speed;
                break;
            case 3:
                readyX -= speed;
                break;
        }
    }
    public virtual void Death() {
        Destroy(this.gameObject);
    }
    public virtual void SetPosition(int _X, int _Y) {

        X = _X;
        Y = _Y;
        transform.localPosition = new Vector3(X * once, Y * once, transform.localPosition.z);

    }
    public void GetVector(int X1, int Y1, int X2, int Y2, ref int V1, ref int V2) {
        if(X1 <= X2) {
            V1 = 1;
        } else {
            V1 = 3;
        }
        if(Y1 <= Y2) {
            V1 = 0;
        } else {
            V1 = 2;
        }
    }
    public virtual void Attack(GameObject _pivot,int damage,typeOfDamage type) {
        Collider2D[][] damagedObjectCollider = new Collider2D[1][];
        /* damagedObjects = Physics2D.OverlapPointAll(_pivot.transform.localPosition);
        
         foreach(Collider2D damagedObject in damagedObjects) {  
             if(damagedObject != null) {
                 if(!damagedObject.isTrigger) {

                 }
             }
         }*/
        objectBase damagedScript;
        GameObject Attacker=this.gameObject;
        damagedObjectCollider[0] = Physics2D.OverlapPointAll(_pivot.transform.position);
        foreach(Collider2D[] damagedObjectList in damagedObjectCollider) {
            foreach(Collider2D damagedObject in damagedObjectList) {
                if(damagedObject != null) {
                    if(!damagedObject.isTrigger) {
                        //Attacker = ;
                        damagedScript = damagedObject.gameObject.transform.parent.gameObject.GetComponent<objectBase>();
                        if(damagedScript != null) {
                            damagedScript.Damaged(damage, type,X,Y,Attacker);
                           

                        }
                    }
                }

            }
        }
    }

}

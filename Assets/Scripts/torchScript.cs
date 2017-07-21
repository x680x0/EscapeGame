using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchScript:objectBase {
    public bool lightOn;
    public GameObject Light;
    CircleCollider2D circleCollider2D;
    public override void Start() {
        base.Start();
        X = (int)(transform.localPosition.x / once);
        Y = (int)(transform.localPosition.y / once);
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    public override void Update() {
        base.Update();
        if(MGR.END) {
            Light.transform.localScale *= 1.01f;
        }
    }
    public void TurnLight(bool on) {
        lightOn = on;

        if(on) {
            circleCollider2D.enabled = on;
            Light.SetActive(on);
            StartCoroutine(ON());
        } else {
            StartCoroutine(OFF());
        }
    }

    public bool GetLight() {
        return lightOn;
    }
    public void AddEnemy(enemyMgr.enemyID _enemy) {
        float Angle = 0; ;
        float r=0;
       bool tmpInLight = true;

        while(tmpInLight) {
            tmpInLight = false;
            r = Random.Range(15f, 18f);
        Angle = Random.Range(0f, 2 * Mathf.PI);
        Collider2D[][] groundCheckCollider = new Collider2D[1][];
            groundCheckCollider[0] = Physics2D.OverlapPointAll(new Vector2(((Mathf.Cos(Angle) * r / once) + X) * once, ((Mathf.Sin(Angle) * r / once) + Y) * once));
            foreach(Collider2D[] groundCheckList in groundCheckCollider) {
                foreach(Collider2D groundCheck in groundCheckList) {
                    if(groundCheck != null) {
                        if(groundCheck.isTrigger) {
                            if(groundCheck.tag == "Light") {
                                tmpInLight = true;
                            }
                        }
                    }

                }
            }
        }
        objectBase a = MGR.EnemyInstantiate(_enemy).GetComponent<objectBase>();
        a.SetPosition((int)(Mathf.Cos(Angle) * r / once) + X, (int)(Mathf.Sin(Angle) * r / once)+Y);
            a.SetTourch(this.gameObject);

    }
    public void AddEnemy(enemyMgr.enemyID _enemy,float Angle,float r) {

        objectBase a = MGR.EnemyInstantiate(_enemy).GetComponent<objectBase>();
        a.SetPosition((int)(Mathf.Cos(Angle) * r / once) + X, (int)(Mathf.Sin(Angle) * r / once) + Y);
        a.SetTourch(this.gameObject);

    }
    public void AddItem(itemMgr.itemID _item, itemMgr.weapnID _weapn) {
        float Angle;
        float r;
        Angle = Random.Range(0f, 2 * Mathf.PI);
        r = Random.Range(0f, 4f);
        objectBase a = MGR.ItemInstantiate(_item,_weapn).GetComponent<objectBase>();
        a.SetPosition((int)(Mathf.Cos(Angle) * r / once) + X, (int)(Mathf.Sin(Angle) * r / once) + Y);
        //  a.SetTourch(this.gameObject);
    }
    public void AddItem(itemMgr.itemID _item, itemMgr.weapnID _weapn,float Angle,float r) {
        objectBase a = MGR.ItemInstantiate(_item, _weapn).GetComponent<objectBase>();
        a.SetPosition((int)(Mathf.Cos(Angle) * r / once) + X, (int)(Mathf.Sin(Angle) * r / once) + Y);
        //  a.SetTourch(this.gameObject);
    }
    /*
    IEnumerator On() {
        yield return null; 
    }*/
    IEnumerator ON() {
        float i = 0.1f;
        while(i < 2.5f) {
            Light.transform.localScale = new Vector3(i, i, 0);
            i += 0.1f;
            yield return null;
        }
    }

    IEnumerator OFF() {
        float i = 2.5f;
        while(i >0) {
            Light.transform.localScale = new Vector3(i, i, 0);
            i -= 0.1f;
            yield return null;
        }
  
        circleCollider2D.enabled = false;
        Light.SetActive(false);
    }
}

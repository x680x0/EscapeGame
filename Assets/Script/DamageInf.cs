using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInf : MonoBehaviour {
    public int damage;
    public bool tui;//弾など、ダメを与えたら壊れるもの用
    public Vector2 nock;
    public float nocktime;
    public GameObject Pivot;
    public float power;

    public bool poison;//毒フラグ
    public int PTimer,PTIME;//毒の場合、PTIMEを設定しなければならない

    void Start() {
        PTimer = 0;
    }
    public int GetDamage() {
        if(PTIME > 0) {
            if(PTimer == 0) {
                return damage;
            }else {
                return 0;
            }
        }else {
            return damage;
        }
    }
    public void GetInf(ref int _damage,ref Vector2 _nock,ref float _nocktime) {
        _damage = damage;
        _nock = nock;
        _nocktime = nocktime;
        if(tui) {
            Destroy(transform.parent.gameObject);
        }
    }
    public void GetInf(ref int _damage, ref Vector2 _nock,ref float _nocktime,Vector2 pivot) {
        _damage = damage;
        _nock =power*( pivot - (Vector2)Pivot.transform.position).normalized;

        _nocktime = nocktime;
        if(tui) {
            Destroy(transform.parent.gameObject);
        }
    }
    public bool GetPoison() {
        return poison;
    }
    void FixedUpdate() {
        if(PTIME > 0) {
            PTimer += 1;
            if(PTimer >= PTIME) {
                PTimer = 0;
            }
        }
    }
}

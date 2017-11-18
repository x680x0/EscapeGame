using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInf : MonoBehaviour {
    public int damage;
    public bool tui;
    public Vector2 nock;
    public float nocktime;
    public GameObject Pivot;
    public float power;
    public int GetDamage() {
        return damage;
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
}

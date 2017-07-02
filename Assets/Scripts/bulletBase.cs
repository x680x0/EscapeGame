using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBase : MonoBehaviour {

    float once;
    int vector;
    Vector2 speed;
    Rigidbody2D rigidbody2D;
    public GameObject pivot;
    int damage;
    objectBase.typeOfDamage type;
    GameObject Attacker;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Fire(int _X,int _Y,int _vector,float _speed,objectBase.typeOfDamage _type,int _damage,GameObject _Attacker) {
        vector = _vector;
        Attacker = _Attacker;
        type = _type;
        damage = _damage;
        rigidbody2D = GetComponent<Rigidbody2D>();
        once = GameObject.Find("mgrObject").GetComponent<mgrScript>().once;
        transform.localPosition = new Vector3(_X * once, _Y * once, transform.localPosition.z);
        Vector2 tmp;
        switch(_vector) {
            case 0:
                tmp = new Vector2(0, _speed);
                break;
            case 1:
                tmp = new Vector2(_speed,0);
                break;
            case 2:
                tmp = new Vector2(0,-_speed);
                break;
            case 3:
                tmp = new Vector2(-_speed, 0);
                break;
            default:
                tmp = new Vector2();
                break;
        }
        speed = rigidbody2D.velocity = tmp;
    }
    public void FixedUpdate() {

        Collider2D[][] damagedObjectCollider = new Collider2D[1][];
        objectBase damagedScript;
        damagedObjectCollider[0] = Physics2D.OverlapPointAll(pivot.transform.position);
        foreach(Collider2D[] damagedObjectList in damagedObjectCollider) {
            foreach(Collider2D damagedObject in damagedObjectList) {
                if(damagedObject != null) {
                    if(!damagedObject.isTrigger) {
                        //Attacker = ;
                        damagedScript = damagedObject.gameObject.transform.parent.gameObject.GetComponent<objectBase>();
                        if(damagedScript != null) {
                            damagedScript.Damaged(damage, type, (int)(transform.localPosition.x/once),(int) (transform.localPosition.y / once), Attacker);
                            Destroy(this.gameObject);
                        }
                    }
                }

            }
        }
    }
    public void Fire(int _X,int _Y,int toX,int toY) {

    }

}

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
    Animator animator;
    static int[] hash;
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        hash = new int[4];
        hash[0] = Animator.StringToHash("up");
        hash[1] = Animator.StringToHash("right");
        hash[2] = Animator.StringToHash("down");
        hash[3] = Animator.StringToHash("left");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Fire(int _X,int _Y,int _vector,float _speed,objectBase.typeOfDamage _type,int _damage,GameObject _Attacker) {
        animator = GetComponent<Animator>();
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
                animator.Play("up");
                tmp = new Vector2(0, _speed);
                break;
            case 1:
                animator.Play("right");
                tmp = new Vector2(_speed,0);
                break;
            case 2:
                animator.Play("down");
                tmp = new Vector2(0,-_speed);
                break;
            case 3:
                animator.Play("left");
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
                        if(damagedScript != null&&damagedScript.gameObject!=Attacker) {
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

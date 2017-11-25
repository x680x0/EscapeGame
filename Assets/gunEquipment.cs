using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunEquipment :WeaaponEquipment {

    public GameObject Bullet;
    int contlol;
    public override void Start() {
        ammo = 10;
        ammoMax = 10;
        weaponType = WeaponType.Gun;
        animator = GetComponent<Animator>();
        blow = new int[4];
        blow[0] = Animator.StringToHash("up");
        blow[1] = Animator.StringToHash("right");
        blow[2] = Animator.StringToHash("down");
        blow[3] = Animator.StringToHash("left");
        none = Animator.StringToHash("none");
        contlol = GetComponent<CNum>().GetContlol();
    }
    public override void StopAnimation() {

        animator.Play(none);
    }
    public void Shot() {
        if(ammo > 0) {
            GameObject bullet = Instantiate(Bullet, transform.position, transform.localRotation);
            bullet.GetComponent<CNum>().ini(contlol);
            bullet.GetComponent<Bullet>().ini(vect);
            ammo--;
        }else {

        }
    }
    public override WeaponType GetWeaponType() {
        return WeaponType.Gun;
    }
}

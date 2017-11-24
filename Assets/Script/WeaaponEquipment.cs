using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaaponEquipment : MonoBehaviour {

    protected Animator animator;
    protected static int[] blow;
    protected static int none;
    public int ammo;
    public int ammoMax;
    public GameObject[] attackCol;
    public int vect;
    public  WeaponType weaponType;
    // Use this for initialization
    public virtual void Start() {
        ammo = 0;
        weaponType = WeaponType.Sword;
        animator = GetComponent<Animator>();
        blow = new int[4];
        blow[0] = Animator.StringToHash("up");
        blow[1] = Animator.StringToHash("right");
        blow[2] = Animator.StringToHash("down");
        blow[3] = Animator.StringToHash("left");
        none = Animator.StringToHash("none");
    }
    public void ini() {

    }
    public void Attack(int vect) {
        attackCol[vect].SetActive(true);
    }


    public virtual void PlayAnimation(int vector) {
        animator.Play(blow[vector]);
        vect = vector;
    }
    public virtual void StopAnimation() {
        animator.Play(none);
        foreach(GameObject attackcol in attackCol) {
           attackcol.SetActive(false);
        }
    }
    public virtual WeaponType GetWeaponType() {
        return weaponType;
    }
    public int GetAmmoMax() {
        return ammoMax;
    }
    public int GetAmmo() {
        return ammo;
    }
    public void SetMax() {
        ammo = ammoMax;
    }
}

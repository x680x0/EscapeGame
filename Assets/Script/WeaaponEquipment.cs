using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaaponEquipment : MonoBehaviour {

    Animator animator;
    static int[] blow;
    static int none;
    public GameObject[] attackCol;
    protected WeaponType weaponType;
    // Use this for initialization
    void Start() {
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


    public void PlayAnimation(int vector) {
        animator.Play(blow[vector]);
       
    }
    public void StopAnimation() {
        animator.Play(none);
        foreach(GameObject attackcol in attackCol) {
           attackcol.SetActive(false);
        }
    }
    public WeaponType GetWeaponType() {
        return weaponType;
    }
}

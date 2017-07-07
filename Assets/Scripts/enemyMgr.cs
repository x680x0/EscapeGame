using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMgr:MonoBehaviour {

    public enum enemyID {
        slime,
        spider
    }

    public GameObject[] enemys;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject EnemyInstantiate(enemyID _enemy) {
        return Instantiate(enemys[(int)_enemy]);
    }
}

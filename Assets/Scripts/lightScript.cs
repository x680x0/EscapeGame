using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour {
    public GameObject[] tourchs;
    torchScript[] lights;
    public float time;
    int wave = 0;
    float nextwave;
    public GameObject mgr;
    mgrScript mgrs;
	// Use this for initialization
	void Start () {
        nextwave = 20f;
        time = 0;
        lights = new torchScript[tourchs.Length];
        int count = 0;
        foreach(GameObject g in tourchs) {
            lights[count] = g.GetComponent<torchScript>();
            count++;
        }
        mgrs = mgr.GetComponent<mgrScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!mgrs.END) {
            time -= Time.deltaTime;
        }
        if(time < 0) {
            time = nextwave;
            if(wave < lights.Length) {
                lights[wave].TurnLight(true);
                switch(wave) {
                    case 0:
                        lights[wave].AddItem(itemMgr.itemID.weapon, itemMgr.weapnID.shortSord,Mathf.PI*3/2,5);

                        break;
                    case 1:
                        lights[wave].AddItem(itemMgr.itemID.stone, itemMgr.weapnID.hardSord, Mathf.PI * 3 / 2, 5);
                        for(int i = 0; i < 5; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                        }
                        break;
                    case 2:
                        for(int i = 0; i < 8; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                        }
                        break;
                    case 3:
                        lights[wave].AddItem(itemMgr.itemID.healBottle, itemMgr.weapnID.hardSord, Mathf.PI * 3 / 2, 5);
                        for(int i = 0; i < 10; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                        }
                        break;
                    case 4:
                        for(int i = 0; i < 4; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                        }
                        for(int i = 0; i < 1; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.spider);
                        }
                        break;
                    case 5:
                        lights[wave].AddItem(itemMgr.itemID.weapon, itemMgr.weapnID.hardSord, Mathf.PI * 3 / 2, 5);
                        lights[wave].AddItem(itemMgr.itemID.stone, itemMgr.weapnID.hardSord, Mathf.PI * 3 / 2, 5);
                        for(int i = 0; i < 8; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                        }
                        for(int i = 0; i < 2; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.spider);
                        }
                        break;
                    case 6:
                        lights[wave].AddItem(itemMgr.itemID.healBottle, itemMgr.weapnID.hardSord, Mathf.PI * 3 / 2, 5);
                        lights[wave].AddItem(itemMgr.itemID.healBottle, itemMgr.weapnID.hardSord, Mathf.PI * 3 / 2, 5);
                        for(int i = 0; i < 10; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                        }
                        for(int i = 0; i < 3; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.spider);
                        }
                        break;
                    case 7:
                        for(int i = 0; i < 15; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                        }
                        for(int i = 0; i < 5; i++) {
                            lights[wave].AddEnemy(enemyMgr.enemyID.spider);
                        }
                        break;
                    case 8:
                        lights[wave].AddItem(itemMgr.itemID.healBottle, itemMgr.weapnID.hardSord, Mathf.PI * 3 / 2, 5);
                        break;
                }
                
            }
            if(wave - 2 >= 0) {
                lights[wave - 2].TurnLight(false);
            }
            wave += 1;
        }
	}
}

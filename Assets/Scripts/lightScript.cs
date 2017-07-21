using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour {
    public GameObject[] tourchs;
    torchScript[] lights;
    public float time;
    int wave = 0;
    float nextwave;
	// Use this for initialization
	void Start () {
        nextwave = 20f;
        time = 0;
        lights = new torchScript[tourchs.Length];
        int count = 0;
        foreach(GameObject g in tourchs) {
            lights[count] = g.GetComponent<torchScript>();
            lights[count].TurnLight(false);
            count++;
        }
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if(time < 0) {
            time = nextwave;
            if(wave < lights.Length) {
                lights[wave].TurnLight(true);
                /*  for(int i = 0; i < 10; i++) {
                        lights[wave].AddEnemy(enemyMgr.enemyID.slime);
                    }
                  for(int i = 0; i < 3; i++) {
                        lights[wave].AddEnemy(enemyMgr.enemyID.spider);
                    }*/
                lights[wave].AddItem(itemMgr.itemID.weapon, itemMgr.weapnID.shortSord   );
            }
            if(wave - 2 >= 0) {
                lights[wave - 2].TurnLight(false);
            }
            wave += 1;
        }
	}
}

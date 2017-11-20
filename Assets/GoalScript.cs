using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

       // StartCoroutine(GameClear());
    }
	
	// Update is called once per frame
	void Update () {
	}
    public IEnumerator GameClear() {
        Vector3 range = new Vector3(5, 5, 0);
        float num = 5;
        for(int i = 0; i < 100; i++) {
            num *= 1.01f;
            range.Set(num, num, 0);
            transform.localScale = range;
            yield return null;
        }
    }
}

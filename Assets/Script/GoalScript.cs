using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript:MonoBehaviour {

    // Use this for initialization
    void Start() {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update() {
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
    public IEnumerator GameStart() {
        Vector3 range = new Vector3(0.5f, 0.5f, 0);
        float num = 0.5f;
        for(int i = 0; i < 100; i++) {
            num *= 1.1f;
            if(num > 5) {
                num = 5;
            }
            range.Set(num, num, 0);
            transform.localScale = range;
            yield return null;
        }
    }
    public IEnumerator GameOver() {
        Vector3 range = new Vector3(5, 5, 0);
        float num = 5;
        for(int i = 0; i < 100; i++) {
            num *= 0.95f;
            range.Set(num, num, 0);
            transform.localScale = range;
            yield return null;
        }

        range.Set(0,0, 0);
        transform.localScale = range;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(0);
    }
}

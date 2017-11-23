using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    SpriteRenderer SR;
	// Use this for initialization
	void Start () {
        SR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        GamepadState state = GamePad.GetState((GamePad.Index)0);
        if(state.A || state.B || state.Y || state.X) {
            StartCoroutine(MOVE());
        }
    }
    IEnumerator MOVE() {
        for(float i = 0; i < 255; i+=1) {
            SR.color = new Color(1,1,1, (float)((255 - i)/255));
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}

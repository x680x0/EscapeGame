using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumを使うために必要
using System;

public class InputCheck:MonoBehaviour {
    /*
    void Update() {
        //   DownKeyCheck();

          if(Input.GetKeyDown(KeyCode.Joystick1Button1)) print("1-1");
          if(Input.GetKeyDown(KeyCode.Joystick2Button1)) print("2-1");
          if(Input.GetKeyDown(KeyCode.Joystick3Button1)) print("3-1");
          if(Input.GetKeyDown(KeyCode.Joystick4Button1)) print("4-1");
          if(Input.GetKeyDown(KeyCode.Joystick5Button1)) print("5-1");
          if(Input.GetKeyDown(KeyCode.Joystick6Button1)) print("6-1");
          if(Input.GetKeyDown(KeyCode.Joystick7Button1)) print("7-1");
          if(Input.GetKeyDown(KeyCode.Joystick8Button1)) print("8-1");
          if(Input.GetKeyDown(KeyCode.JoystickButton1)) print("?-1");

        
    }


    void DownKeyCheck() {
      if(Input.anyKeyDown) {
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode))) {
                if(Input.GetKeyDown(code)) {
                    //処理を書く
                    Debug.Log(code);
                    break;
                }
            }
        }
    }*/
    
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // 接続されているコントローラの名前を調べる
        var controllerNames = Input.GetJoystickNames();

        // 一台もコントローラが接続されていなければエラー
        if(controllerNames[0] == "") Debug.Log("Error");
        Debug.Log(controllerNames[0]);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healBottleScript :itemBase {

    public override void Shock(GameObject _gameObject) {
        switch(_gameObject.tag) {
            case "Player":
                //回復処理
                break;
            default:
                break;
        }
    }
    public override void Grounded() {
        base.Grounded();
        Destroy(this.gameObject);
    }
}

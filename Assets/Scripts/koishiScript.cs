using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koishiScript :itemBase {
    public override void Shock(GameObject _gameObject) {
        switch(_gameObject.tag) {
            case "Enemy":
                _gameObject.GetComponent<objectBase>().Damaged(10, objectBase.typeOfDamage.cross, X, Y, actor, this.gameObject.tag);
                break;
            default:
                break;
        }
    }
}

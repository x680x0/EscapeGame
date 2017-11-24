using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct GUIFrame {
	public Image character;
	public Image lifeGuage;
	public Text lifeText;
	public Image item;
	public Image weapon;
	public Text ammoNow;
	public Text ammoMax;
}

public enum CharacterType {
	None = -1,
	Kinpatu,
	Ginpatu
}

public enum WeaponType {
	None = -1,
	Sword,
    Gun
}

public enum ItemType {
	None = -1,
	Portion
}

public class Status {
	public CharacterType character;
	public int hp;
	public ItemType item;
	public WeaponType weapon;
	public int ammoNow;
	public int ammoMax;
}

public class GUIControl : MonoBehaviour {

	GUIFrame[] frames = new GUIFrame[4];

	Sprite[] characters = new Sprite[2];
	Sprite[] weapons = new Sprite[2];
	Sprite[] items = new Sprite[1];
	Sprite None;

	// Use this for initialization
	void Awake () {
		//Resources読み込み
		characters[0] = Resources.Load<Sprite>("GUISprites/character_0");
		characters[1] = Resources.Load<Sprite>("GUISprites/character_1");
		weapons[0] = Resources.Load<Sprite>("GUISprites/weapon_sword");
        weapons[1] = Resources.Load<Sprite>("GUISprites/weapon_gun");
        items[0] = Resources.Load<Sprite>("GUISprites/item_portion");
		None = Resources.Load<Sprite>("GUISprites/none");

		//インスタンス捕捉
		for (int i = 0; i < frames.Length; i++) {
			frames [i].character = transform.Find ("frame_" + (i + 1).ToString () + "p/character").gameObject.GetComponent<Image>();
			frames [i].lifeGuage = transform.Find ("frame_" + (i + 1).ToString () + "p/life_guage").gameObject.GetComponent<Image> ();
			frames [i].lifeText = transform.Find ("frame_" + (i + 1).ToString () + "p/life_text").gameObject.GetComponent<Text> ();
			frames [i].item = transform.Find ("frame_" + (i + 1).ToString () + "p/item").gameObject.GetComponent<Image> ();
			frames [i].weapon = transform.Find ("frame_" + (i + 1).ToString () + "p/weapon").gameObject.GetComponent<Image> ();
			frames [i].ammoNow = transform.Find ("frame_" + (i + 1).ToString () + "p/ammo_now").gameObject.GetComponent<Text> ();
			frames [i].ammoMax = transform.Find ("frame_" + (i + 1).ToString () + "p/ammo_max").gameObject.GetComponent<Text> ();
		}
	}
	
	public void SetStatusAll (Status[] status){
		for (int i = 0; i < status.Length; i++) {
			frames [i].character.sprite = ((int)status [i].character < 0) ? None : characters [(int)status [i].character];
			frames [i].weapon.sprite = ((int)status [i].weapon < 0) ? None : weapons [(int)status [i].weapon];
			frames [i].item.sprite = ((int)status [i].item < 0) ? None : items [(int)status [i].item];

			frames [i].lifeText.text = status [i].hp.ToString ();
			frames [i].lifeGuage.fillAmount = (float)status [i].hp / 100f;

			frames [i].ammoNow.text = status [i].ammoNow.ToString ();
			frames [i].ammoMax.text = status [i].ammoMax.ToString ();
		}
	}

	public void SetStatus (Status status, int index){
		frames [index].character.sprite = ((int)status.character < 0) ? None : characters [(int)status.character];
		frames [index].weapon.sprite = ((int)status.weapon < 0) ? None : weapons [(int)status.weapon];
		frames [index].item.sprite = ((int)status.item < 0) ? None : items [(int)status.item];

		frames [index].lifeText.text = status.hp.ToString ();
		frames [index].lifeGuage.fillAmount = (float)status.hp / 100f;

		frames [index].ammoNow.text = status.ammoNow.ToString ();
		frames [index].ammoMax.text = status.ammoMax.ToString ();
	}

	public void SetItem (ItemType item, int index){
		frames[index].item.sprite = ((int)item < 0) ? None : items [(int)item];
	}

	public void SetWeapon (WeaponType weapon, int index){
		frames [index].weapon.sprite = ((int)weapon < 0) ? None : weapons [(int)weapon];
	}

	public void SetDamage (int damageAmount, int index){
		int life = Mathf.Clamp (int.Parse (frames [index].lifeText.text) - damageAmount, 0, 100);
		frames [index].lifeText.text = life.ToString ();
		frames [index].lifeGuage.fillAmount = (float)life / 100f;
	}
    
	public void SetAmmoMax(int newAmmoMax,int index){
		frames [index].ammoMax.text = newAmmoMax.ToString();
		frames [index].ammoNow.text = newAmmoMax.ToString();
	}
	public void SetUseAmmo(int UseAmount,int index){
		int Ammo = Mathf.Clamp (int.Parse (frames [index].ammoNow.text) - UseAmount, 0, int.Parse (frames [index].ammoMax.text));
		frames [index].ammoNow.text = Ammo.ToString ();
	}

}

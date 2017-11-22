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
	Girl
}

public enum WeaponType {
	None = -1,
	Sword
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

	Sprite[] characters = new Sprite[1];
	Sprite[] weapons = new Sprite[1];
	Sprite[] items = new Sprite[1];
	Sprite None;

	// Use this for initialization
	void Start () {
		//Resources読み込み
		characters[0] = Resources.Load<Sprite>("GUISprites/character_0");
		weapons[0] = Resources.Load<Sprite>("GUISprites/weapon_sword");
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
	
	public void SetStatus (Status[] status){
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelector : MonoBehaviour {

	public ItemInfo item;
	public GameObject num;
	public int itemAmt;
	private Vector3 mousePos;

	// Use this for initialization
	void Start () {
		//item = null;
		num = transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Input.mousePosition;
		transform.position = new Vector3(mousePos.x + 30, mousePos.y - 30, mousePos.z);
	}

	public void DisplaySelector(ItemInfo itemToDisplay, int amount){
		transform.position = Input.mousePosition;
		item.SetEqualTo(item, itemToDisplay);
		gameObject.GetComponent<Image> ().sprite = item.icon;
		itemAmt = amount;
		if (itemAmt > 1) {
			num.GetComponent<Text> ().text = itemAmt + "";
		} else {
			num.GetComponent<Text> ().text = "";
		}
	}

	public void ChangeSelectorAmt(int amount){
		itemAmt += amount;
		if (itemAmt > 1) {
			num.GetComponent<Text> ().text = itemAmt + "";
		} else {
			num.GetComponent<Text> ().text = "";
		}
	}
}

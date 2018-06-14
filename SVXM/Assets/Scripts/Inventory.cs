using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public Slot[] invSlots = new Slot[32];
	public Slot[] toolSlots = new Slot[9];
	public int currentSlot;
	public GameObject invSlotsParent;
	public GameObject toolSlotsParent;
	public GameObject currentSelecObject;
	public bool shift;


	// Use this for initialization
	void Start () {
		AttachSlots ();
		UpdateMenu ();
		currentSelecObject.SetActive (false);
		currentSlot = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			shift = true;
		} else {
			shift = false;
		}
		for (int i = 0; i < 9; i++) {
			if (Input.GetButtonDown ("" + (i+1))) {
				ChangeCurrentSlot (i);
			}
		}
		if (Input.GetButtonDown ("T")) {
			MenuController menu = Camera.main.GetComponent<MenuController> ();
			if (!menu.menuOpen && toolSlots[currentSlot].item.title != "") {
				UseItem (toolSlots[currentSlot].item);
			}
		}
	}

	void UpdateMenu(){
		//Inventory Slots
		for (int i = 0; i < invSlots.Length; i++) {
			GameObject icon = invSlots [i].slotObject.transform.GetChild (0).gameObject;
			if (invSlots [i].item.title != "") {
				icon.GetComponent<Image>().sprite = invSlots [i].item.icon;
				icon.SetActive (true);
				if (invSlots[i].item.stackable) {
					icon.transform.GetChild (0).GetComponent<Text> ().text = invSlots [i].amount + "";
				} else {
					icon.transform.GetChild (0).GetComponent<Text> ().text = "";
				}			} else {
				icon.SetActive (false);
			}
		}

		//Tool Bar Slots
		for (int i = 0; i < toolSlots.Length; i++) {
			GameObject icon = toolSlots [i].slotObject.transform.GetChild (1).gameObject;
			if (i == currentSlot) {
				toolSlots [i].slotObject.transform.GetChild (0).gameObject.SetActive (true);
			} else {
				toolSlots [i].slotObject.transform.GetChild (0).gameObject.SetActive (false);
			}
			if (toolSlots [i].item.title != "") {
				icon.GetComponent<Image>().sprite = toolSlots [i].item.icon;
				icon.SetActive (true);
				if (toolSlots[i].item.stackable) {
					icon.transform.GetChild (0).GetComponent<Text> ().text = toolSlots [i].amount + "";
				} else {
					icon.transform.GetChild (0).GetComponent<Text> ().text = "";
				}
			} else {
				icon.SetActive (false);
			}
		}
	}

	void UpdateSlot(int slotNum, bool invSlot){
		if (invSlot) {
			GameObject icon = invSlots [slotNum].slotObject.transform.GetChild (0).gameObject;
			if (invSlots [slotNum].item.title != "") {
				icon.SetActive (true);
				icon.GetComponent<Image> ().sprite = invSlots [slotNum].item.icon;
				if (invSlots [slotNum].amount > 1) {
					icon.transform.GetChild (0).GetComponent<Text> ().text = invSlots [slotNum].amount + "";
				} else {
					icon.transform.GetChild (0).GetComponent<Text> ().text = "";
				}
			} else {
				icon.SetActive (false);
			}
		} else {
			GameObject icon = toolSlots [slotNum].slotObject.transform.GetChild (1).gameObject;
			if (slotNum == currentSlot) {
				toolSlots [slotNum].slotObject.transform.GetChild (0).gameObject.SetActive (true);
			} else {
				toolSlots [slotNum].slotObject.transform.GetChild (0).gameObject.SetActive (false);
			}
			if (toolSlots [slotNum].item.title != "") {
				icon.SetActive (true);
				icon.GetComponent<Image> ().sprite = toolSlots [slotNum].item.icon;
				if (toolSlots [slotNum].amount > 1) {
					icon.transform.GetChild (0).GetComponent<Text> ().text = toolSlots [slotNum].amount + "";
				} else {
					icon.transform.GetChild (0).GetComponent<Text> ().text = "";
				}
			} else {
				icon.SetActive (false);
			}
		}
	}

	void AttachSlots(){
		for (int i = 0; i < invSlots.Length; i++) {
			invSlots [i].slotObject = invSlotsParent.transform.GetChild (i).gameObject;
		}
		for (int i = 0; i < toolSlots.Length; i++) {
			toolSlots [i].slotObject = toolSlotsParent.transform.GetChild (i).gameObject;
		}
	}

	public void ClickedInvSlot(int slotIndex){
		InventorySelector sel = currentSelecObject.GetComponent<InventorySelector> ();
		if (invSlots [slotIndex].item.title == "") {
			if (currentSelecObject.activeSelf == true) {
				invSlots [slotIndex].item.SetEqualTo(invSlots[slotIndex].item, sel.item);
				if (shift && sel.item.stackable) {
					invSlots [slotIndex].amount++;
					sel.ChangeSelectorAmt(-1);
					if (sel.itemAmt == 0) {
						currentSelecObject.SetActive (false);
					}
				} else {
					invSlots [slotIndex].amount = sel.itemAmt;
					currentSelecObject.SetActive (false);
				}
			}
		} else if (invSlots [slotIndex].item.title != "") {
			if (currentSelecObject.activeSelf == true) {
				if (invSlots [slotIndex].item.title == sel.item.title && sel.item.stackable) {
					if (shift && sel.itemAmt > 1) {
						sel.ChangeSelectorAmt(-1);
						invSlots [slotIndex].amount++;
					} else {
						invSlots[slotIndex].amount += sel.itemAmt;
						currentSelecObject.SetActive (false);
					}
				}
			} else {
				currentSelecObject.SetActive(true);
				currentSelecObject.GetComponent<InventorySelector> ().DisplaySelector (invSlots [slotIndex].item, invSlots [slotIndex].amount);
				invSlots [slotIndex].item.Clear();
				invSlots [slotIndex].amount = 0;
			}
		}
		UpdateSlot (slotIndex, true);
	}

	public void ClickedToolSlot(int slotIndex){
		InventorySelector sel = currentSelecObject.GetComponent<InventorySelector> ();
		if (toolSlots [slotIndex].item.title == "") {
			if (currentSelecObject.activeSelf == true) {
				toolSlots [slotIndex].item.SetEqualTo(toolSlots[slotIndex].item, sel.item);
				if (shift && sel.item.stackable) {
					toolSlots [slotIndex].amount++;
					sel.ChangeSelectorAmt(-1);
					if (sel.itemAmt == 0) {
						currentSelecObject.SetActive (false);
					}
				} else {
					toolSlots [slotIndex].amount = sel.itemAmt;
					currentSelecObject.SetActive (false);
				}
			}
		} else if (toolSlots [slotIndex].item.title != "") {
			if (currentSelecObject.activeSelf == true) {
				if (toolSlots [slotIndex].item.title == sel.item.title && sel.item.stackable) {
					if (shift && sel.itemAmt > 1) {
						sel.ChangeSelectorAmt(-1);
						toolSlots [slotIndex].amount++;
					} else {
						toolSlots[slotIndex].amount += sel.itemAmt;
						currentSelecObject.SetActive (false);
					}
				}
			} else {
				currentSelecObject.SetActive(true);
				currentSelecObject.GetComponent<InventorySelector> ().DisplaySelector (toolSlots [slotIndex].item, toolSlots [slotIndex].amount);
				toolSlots [slotIndex].item.Clear();
				toolSlots [slotIndex].amount = 0;
			}
		}
		UpdateSlot (slotIndex, false);	
	}

	public bool AddItem(ItemInfo item){
		bool added = false;
		for (int i = 0; i < invSlots.Length; i++) {
			if (invSlots [i].item.title == "") {
				invSlots [i].item.SetEqualTo(invSlots[i].item, item);
				invSlots [i].amount++;
				UpdateSlot (i, true);
				added = true;
				i = invSlots.Length;
			} else if (invSlots [i].item.title == item.title && item.stackable) {
				invSlots [i].amount++;
				GameObject icon = invSlots [i].slotObject.transform.GetChild (0).gameObject;
				icon.transform.GetChild (0).GetComponent<Text> ().text = invSlots [i].amount + "";
				added = true;
				i = invSlots.Length;
			}
		}
		return added;
	}

	public void RemoveItem(int index, bool invSlot){
		if (invSlot) {
			if (invSlots [index].amount > 1) {
				invSlots [index].amount--;
			} else {
				invSlots [index].item.Clear ();
				invSlots [index].amount--;
			}
		} else {
			if (toolSlots [index].amount > 1) {
				toolSlots [index].amount--;
			} else {
				toolSlots [index].item.Clear ();
				toolSlots [index].amount--;
			}
		}
		UpdateSlot (index, invSlot);
	}

	public void ClickedDrop(){
		if (currentSelecObject.activeSelf == true) {
			InventorySelector sel = currentSelecObject.GetComponent<InventorySelector> ();
			if (shift && sel.itemAmt > 1) {
				Drop (sel.item, 1);
				sel.ChangeSelectorAmt (-1);
			} else {
				Drop (sel.item, sel.itemAmt);
				currentSelecObject.SetActive (false);
			}
		}
	}

	public void Drop(ItemInfo item, int amount){
		for (int i = 0; i < amount; i++) {
			float newX = transform.position.x + (float)Random.Range (-25, 25);
			float newY = transform.position.y + (float)Random.Range (-25, 25);
			Vector3 spawnLocation = new Vector3(newX, newY, transform.position.z);
			GameObject it = (GameObject)Instantiate (Resources.Load("Prefabs/Items/" + item.prefabTitle), spawnLocation, Quaternion.identity);
			it.GetComponent<SceneItem> ().allowPickUp = false;
			it.GetComponent<SceneItem>().justDropped = true;
		}
	}

	public void ChangeCurrentSlot(int newSlotNum){
		toolSlots [currentSlot].slotObject.transform.GetChild (0).gameObject.SetActive (false);
		currentSlot = newSlotNum;
		toolSlots [currentSlot].slotObject.transform.GetChild (0).gameObject.SetActive (true);
	}

	void UseItem(ItemInfo item){
		PlayerStats stat = gameObject.GetComponent<PlayerStats> ();
		switch (toolSlots [currentSlot].item.itemType) {
		case ItemInfo.Types.Consumable:
			stat.ChangeHealth (item.foodInfo.healthReco);
			stat.ChangeStamina (item.foodInfo.staminaReco);
			stat.ChangeHunger (item.foodInfo.hungerReco);
			stat.ChangeThirst (item.foodInfo.thirstReco);
			RemoveItem (currentSlot, false);
			break;
		}
	}
}

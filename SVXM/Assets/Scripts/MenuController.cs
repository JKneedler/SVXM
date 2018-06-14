using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public GameObject menuObject;
	public bool menuOpen;
	public enum MenuOptions{ Inventory, Crafting, Skills, Magic, Stats, Settings}
	public GameObject[] menuScreens;
	public GameObject player;

	// Use this for initialization
	void Start () {
		menuOpen = false;
		menuObject.SetActive (menuOpen);
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("E")) {
			menuOpen = !menuOpen;
			menuObject.SetActive (menuOpen);
			if (!menuOpen) {
				Inventory inv = player.GetComponent<Inventory> ();
				if (inv.currentSelecObject.activeSelf == true) {
					InventorySelector sel = inv.currentSelecObject.GetComponent<InventorySelector> ();
					inv.Drop (sel.item, sel.itemAmt);
					inv.currentSelecObject.SetActive (false);
				}
			}
		}
	}

	public void DisableAllBut(int screenNum){
		for (int i = 0; i < menuScreens.Length; i++) {
			if (menuScreens [i] != null) {
				if (i == screenNum) {
					menuScreens [i].SetActive (true);
				} else {
					menuScreens [i].SetActive (false);
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfo : MonoBehaviour {

	public BreakDownItem bDItem;
	public SoilTile cropInfo;
	public bool isAbleToBeTilled;
	private GameObject player;

	// Use this for initialization
	void Start () {
		bDItem.parentTile = gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (bDItem.adjustSortingLayer) {
			int sortingOrderInt;
			float sortingOrderFloat = player.transform.position.y - transform.position.y;
			if (sortingOrderFloat > 0) {
				sortingOrderInt = 1;
			} else {
				sortingOrderInt = 0;
			}
			gameObject.transform.GetChild(0).GetComponent<SpriteRenderer> ().sortingOrder = sortingOrderInt;
		}
		if (cropInfo.hasCrop) {
			if (cropInfo.plantTimer > 0) {
				cropInfo.plantTimer -= Time.deltaTime;
			}
			if (cropInfo.plantTimer <= 0) {
				if (cropInfo.currentStage >= 4) {
				} else {
					cropInfo.currentStage++;
					cropInfo.plantTimer = cropInfo.crop.growingTime;
					gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = cropInfo.crop.stageSprites [cropInfo.currentStage-1];
				}
			}
		}
	}

	public void ActivateTile(GameObject player, ItemInfo currentItem){
		if (bDItem.hasItem) {
			bDItem.HitWithItem (currentItem);
		} else {
			switch (currentItem.itemType) {
			case ItemInfo.Types.Tool:
				switch (currentItem.toolType) {
				case ItemInfo.ToolTypes.Pickaxe:

					break;
				case ItemInfo.ToolTypes.Axe:
				
					break;
				case ItemInfo.ToolTypes.Hoe:
					if (!bDItem.hasItem && isAbleToBeTilled) {
						if (!cropInfo.tilled) {
							SillTile ();
						}
					}
					break;
				case ItemInfo.ToolTypes.FishingRod:
				
					break;
				}
				break;
			case ItemInfo.Types.Seed:
				if (cropInfo.tilled) {
					if (!cropInfo.hasCrop) {
						Plant (player, currentItem);
					}
				}
				break;
			case ItemInfo.Types.BuildingBlock:
			
				break;
			}
		}
	}

	public void ActivateTile(GameObject player){
		PlayerStats stat = player.GetComponent<PlayerStats> ();
		if (bDItem.hasItem) {
			bDItem.Damage(stat.armStrength);
		} else {
			
		}
	}

	public void DropItem(Object itemToDrop){
		GameObject drop = (GameObject)Instantiate (itemToDrop, transform.position, Quaternion.identity);
		drop.GetComponent<SceneItem> ().pickUpTimer = 1;
	}

	public void TakeDownBreakDownItem(){
		transform.GetChild (0).gameObject.GetComponent<SpriteRenderer>().sprite = null;
		transform.GetChild (0).GetComponent<BoxCollider2D> ().enabled = false;
		bDItem.hasItem = false;
	}

	public void SillTile(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = cropInfo.soilSprite;
		cropInfo.tilled = true;
	}

	public void Plant(GameObject player, ItemInfo cropToPlant){
		Inventory inv = player.GetComponent<Inventory> ();
		cropInfo.crop.SetEqualTo (cropInfo.crop, cropToPlant.cropInfo);
		gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = cropInfo.crop.stageSprites [0];
		cropInfo.plantTimer = cropInfo.crop.growingTime;
		cropInfo.currentStage = 1;
		cropInfo.hasCrop = true;
	}
}

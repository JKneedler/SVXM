using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreakDownItem {

	public bool hasItem;
	public Sprite icon;
	public ItemInfo.ToolTypes recommendedTool;
	public int curDur;
	public int maxDur;
	public Drop[] drops;
	public GameObject parentTile;
	public bool adjustSortingLayer;


	public void Damage(int damage){
		curDur -= damage;
		if (curDur <= 0) {
			BreakDown ();
			parentTile.GetComponent<TileInfo> ().TakeDownBreakDownItem ();
		}
	}

	public void HitWithItem(ItemInfo currentItem){
		if (currentItem.toolType == recommendedTool) {
			Damage (currentItem.hitPower);
		} else {
			Damage (currentItem.hitPower / 5);
		}
	}

	void BreakDown(){
		for (int i = 0; i < drops.Length; i++) {
			int amtToDrop = Random.Range (drops [i].minDrops, drops [i].maxDrops + 1);
			for(int k = 0; k < amtToDrop; k++){
				parentTile.GetComponent<TileInfo>().DropItem(drops[i].item);
			}
		}
	}
}

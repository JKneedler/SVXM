using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo {
	public string title;
	public string prefabTitle;
	public Sprite icon;
	public bool stackable;
	public Types itemType;
	public ToolTypes toolType;
	public int hitPower;
	public int damage;
	public bool isConsumable;
	public Consumable foodInfo;
	public Crop cropInfo;

	public enum Types
	{
		Tool,
		Weapon,
		Armor,
		Consumable,
		Seed,
		BuildingBlock
	}

	public enum ToolTypes
	{
		Pickaxe,
		Axe,
		Hoe,
		FishingRod,
		NotTool
	}


	public void Clear(){
		title = "";
		icon = null;
		stackable = false;
		prefabTitle = "";
		hitPower = 0;
		damage = 0;
	}

	public void SetEqualTo(ItemInfo it1, ItemInfo it2){
		it1.title = it2.title;
		it1.icon = it2.icon;
		it1.stackable = it2.stackable;
		it1.prefabTitle = it2.prefabTitle;
		it1.itemType = it2.itemType;
		it1.toolType = it2.toolType;
		it1.hitPower = it2.hitPower;
		it1.damage = it2.damage;
		it1.isConsumable = it2.isConsumable;
		foodInfo.SetEqualTo (it1.foodInfo, it2.foodInfo);
		cropInfo.SetEqualTo (it1.cropInfo, it2.cropInfo);
	}
}

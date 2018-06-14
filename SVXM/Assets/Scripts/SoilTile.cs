using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoilTile {

	public Crop crop;
	public bool tilled;
	public bool watered;
	public bool hasCrop;
	public Sprite soilSprite;
	public int currentStage;
	public float plantTimer;

}

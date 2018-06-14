using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Crop {

	public float growingTime;
	public Sprite[] stageSprites;
	public GameObject growItem;


	public void SetEqualTo(Crop c1, Crop c2){
		c1.growingTime = c2.growingTime;
		c1.stageSprites = c2.stageSprites;
		c1.growItem = c2.growItem;
	}
}

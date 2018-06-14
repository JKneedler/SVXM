using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Consumable {

	public float healthReco;
	public float staminaReco;
	public float hungerReco;
	public float thirstReco;

	public void SetEqualTo(Consumable c1, Consumable c2){
		c1.healthReco = c2.healthReco;
		c1.staminaReco = c2.staminaReco;
		c1.hungerReco = c2.hungerReco;
		c1.thirstReco = c2.thirstReco;
	}
}

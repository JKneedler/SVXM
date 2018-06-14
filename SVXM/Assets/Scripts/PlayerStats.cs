using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public float curHealth;
	public float maxHealth;
	public GameObject healthBar;
	private Vector2 healthBarRect;
	public float curStamina;
	public float maxStamina;
	public GameObject staminaBar;
	private Vector2 staminaBarRect;
	public float curHunger;
	public float maxHunger;
	public GameObject hungerBar;
	private Vector2 hungerBarRect;
	public float curThirst;
	public float maxThirst;
	public GameObject thirstBar;
	private Vector2 thirstBarRect;
	public int armStrength;

	// Use this for initialization
	void Start () {
		healthBarRect = healthBar.GetComponent<RectTransform> ().sizeDelta;
		staminaBarRect = staminaBar.GetComponent<RectTransform> ().sizeDelta;
		hungerBarRect = hungerBar.GetComponent<RectTransform> ().sizeDelta;
		thirstBarRect = thirstBar.GetComponent<RectTransform> ().sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateVitalValues ();
	}

	void UpdateVitalValues(){
		//Health
		if (curHealth > maxHealth) curHealth = maxHealth;
		if (curHealth >= 0) {
			float newWidth = (curHealth / maxHealth) * healthBarRect.x;
			healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (newWidth, healthBarRect.y);
		}
		if (curHealth <= 0) Debug.Log("Die");

		//Stamina
		if (curStamina > maxStamina) curStamina = maxStamina;
		if (curStamina >= 0) {
			float newWidth = (curStamina / maxStamina) * staminaBarRect.x;
			staminaBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (newWidth, staminaBarRect.y);
		}
		if (curStamina <= 0) Debug.Log("Out of Stamina");
		//Hunger
		if (curHunger > maxHunger) curHunger = maxHunger; 
		if (curHunger >= 0) {
			float newHeight = (curHunger / maxHunger) * hungerBarRect.y;
			hungerBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (hungerBarRect.x, newHeight);
		}
		if (curHunger <= 0) Debug.Log("Die from Hunger");
		//Thirst
		if (curThirst > maxThirst) curThirst = maxThirst;
		if (curThirst >= 0) {
			float newHeight = (curThirst / maxThirst) * thirstBarRect.y;
			thirstBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (thirstBarRect.x, newHeight);
		}
		if (curThirst <= 0) Debug.Log("Die from Thirst");
	}

	public void ChangeHealth(float amount){
		curHealth += amount;
		UpdateVitalValues ();
	}

	public void ChangeStamina(float amount){
		curStamina += amount;
		UpdateVitalValues ();
	}

	public void ChangeHunger(float amount){
		curHunger += amount;
		UpdateVitalValues ();
	}

	public void ChangeThirst(float amount){
		curThirst += amount;
		UpdateVitalValues ();
	}
}

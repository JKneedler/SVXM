using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour {

	public ItemInfo item;
	private GameObject player;
	public bool allowPickUp;
	public bool justDropped;
	public float pickUpTimer;


	void Start () {

	}

	void Update () {
		if (player != null && pickUpTimer <= 0 && allowPickUp) {
			transform.position = Vector2.Lerp ((Vector2)transform.position, (Vector2)player.transform.position, .2f);
			if(Vector2.Distance((Vector2)transform.position, (Vector2)player.transform.position) < 10){
				bool added = player.GetComponent<Inventory> ().AddItem (this.item);
				Debug.Log (added);
				Destroy (gameObject);
			}
		}
		if (justDropped) {
			allowPickUp = false;
		}
		if (pickUpTimer > 0) {
			pickUpTimer -= Time.deltaTime;
		}
		if (pickUpTimer <= 0) {
			pickUpTimer = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (allowPickUp) {
			if (collision.gameObject.tag == "Player") {
				player = collision.gameObject;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision){
		if (justDropped) {
			if (collision.gameObject.tag == "Player") {
				justDropped = false;
				allowPickUp = true;
			}
		}
	}

}

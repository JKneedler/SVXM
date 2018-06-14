using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool ableToMove;
	public bool moving;
	private Animator anim;
	public float speed;
	public Vector2 direction;
	public float tileSize;
	public Vector2 endPosititon;
	public enum TileDirections{ Top, Left, Bottom, Right }
	public GameObject[] surroundingTiles = new GameObject[4];
	private Rigidbody2D myRigid;
	public Object outlinePrefab;
	private GameObject outline;
	private float checkTilesTimer;
	public float angle;
	public GameObject currentHoverObject;

	// Use this for initialization
	void Start () {
		ableToMove = true;
		moving = false;
		endPosititon = transform.position;
		//anim = gameObject.GetComponent<Animator> ();
		direction.Set(0, 0);
		myRigid = gameObject.GetComponent<Rigidbody2D> ();
		GetSurroundingTiles ();
	}
	
	// Update is called once per frame
	void Update () {
		//Getting Mouse Angle
		var mouse = Input.mousePosition;
		var screenPoint = Camera.main.WorldToScreenPoint (transform.localPosition);
		var offset = new Vector2 (mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		angle = Mathf.Atan2 (offset.y, offset.x) * Mathf.Rad2Deg;

		//Getting Mouse Position
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);
		mousePos.z = 0;
		RaycastHit2D tileHit = new RaycastHit2D ();
		tileHit = Physics2D.Raycast ((Vector2)mousePos, new Vector2 (0, 1), .5f, 1);
		if (tileHit && tileHit.collider.gameObject.tag == "Player") {
			currentHoverObject = tileHit.collider.gameObject;
		} else {
			if (angle < 140 && angle > 35) currentHoverObject = surroundingTiles [(int)TileDirections.Top];
			if (angle > 140 || angle < -115) currentHoverObject = surroundingTiles [(int)TileDirections.Left];
			if (angle < -55 && angle > -115) currentHoverObject = surroundingTiles [(int)TileDirections.Bottom];
			if (angle < 35 && angle > -55) currentHoverObject = surroundingTiles [(int)TileDirections.Right];
		}

		//Setting currentHoverTile based on angle

		//Setting outline on currentHoverTile
		if (currentHoverObject != null && outline != null) {
			outline.transform.position = currentHoverObject.transform.position;
		}
		if (currentHoverObject == null && outline != null) {
			Destroy (outline);
		}
		if (currentHoverObject != null && outline == null) {
			if (currentHoverObject.GetComponent<TileInfo> () != null) {
				outline = (GameObject)Instantiate (outlinePrefab, currentHoverObject.transform.position, Quaternion.identity);
			}
		}

		if (Input.GetButtonDown ("Fire1")) {
			Inventory inv = gameObject.GetComponent<Inventory> ();
			MenuController menu = Camera.main.GetComponent<MenuController> ();
			if (!menu.menuOpen) {
				if (inv.toolSlots [inv.currentSlot].item.title == "") {
					currentHoverObject.GetComponent<TileInfo> ().ActivateTile (gameObject);
				} else {
					currentHoverObject.GetComponent<TileInfo> ().ActivateTile (gameObject, inv.toolSlots [inv.currentSlot].item);
				}
			}
		}

		//Movement
		if (Input.GetButton ("W")) {
			direction.y = 1;
		} else if (Input.GetButton ("S")) {
			direction.y = -1;
		} else {
			direction.y = 0;
		}
		if (Input.GetButton ("D")) {
			direction.x = 1;
		} else if (Input.GetButton ("A")) {
			direction.x = -1;
		} else {
			direction.x = 0;
		}
		if ((direction.x != 0 || direction.y != 0) && ableToMove) {
			moving = true;
			Move ();
			GetSurroundingTiles ();
		} else {
			myRigid.velocity = new Vector2 (0, 0);
			moving = false;
			if (checkTilesTimer > 0) {
				GetSurroundingTiles ();
				checkTilesTimer -= Time.deltaTime;
			}
			if (checkTilesTimer <= 0) checkTilesTimer = 0;
		}
	}
	void Move () {
		myRigid.velocity = direction * speed;
		checkTilesTimer = .2f;
	}

	void GetSurroundingTiles () {
		RaycastHit2D[] tileHits = new RaycastHit2D[4];
		tileHits [0] = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y + (tileSize) - 25), new Vector2 (0, 1), .5f, 1);
		tileHits [1] = Physics2D.Raycast (new Vector2(transform.position.x - (tileSize), transform.position.y - 25), new Vector2 (-1, 0), .5f, 1);
		tileHits [2] = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y - (tileSize) - 25), new Vector2 (0, -1), .5f, 1);
		tileHits [3] = Physics2D.Raycast (new Vector2(transform.position.x + (tileSize), transform.position.y - 25), new Vector2 (1, 0), .5f, 1);
		for (int i = 0; i < tileHits.Length; i++) {
			if (tileHits [i].collider != null && tileHits[i].collider.gameObject.GetComponent<TileInfo>() != null) {
				surroundingTiles[i] = tileHits [i].collider.gameObject;
			}
		}
	}
		
}
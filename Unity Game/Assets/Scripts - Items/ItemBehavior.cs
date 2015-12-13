﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemBehavior : MonoBehaviour {

	public Item item;
	private bool followMouse = false;
//	private GameObject inventoryButton;
	private Collider trash;
	private SpriteRenderer spriteRenderer;
//	private InventoryController inventoryController;

	/// <summary>
	/// Initializes various variables for later access
	/// </summary>
	void Start(){
		item = new Item("Food"); // TODO: remove from testing
		//TODO: give it a random rotting time
//		inventoryButton = GameObject.FindGameObjectWithTag("InventoryButton");
		trash = GameObject.FindGameObjectWithTag("Trash").GetComponent<Collider>();
		// TODO: uncomment this and make it work
//		inventoryController = GameObject.FindGameObjectWithTag("InventoryController").GetComponent<InventoryController>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// This should be called every time a new ItemBehavior is instantiated so
	/// that it is linked to an Item object and associated with GameData.inventory
	/// </summary>
	/// <param name="pItem">pItem.</param>
	public void SetUp(Item pItem){
		item = pItem;
//		spriteRenderer.sprite = Resources.Load<Sprite>("Textures/" + pItem.textureName);
//		inventoryButton = GameObject.FindGameObjectWithTag("InventoryButton");
		trash = GameObject.FindGameObjectWithTag("Trash").GetComponent<Collider>();
//		inventoryController = GameObject.Find("InventoryController").GetComponent<InventoryController>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown(){
		followMouse = true;
	}

	/// <summary>
	/// Stops following the mouse.
	/// If mouse is over trash, destroy item.
	/// If mouse is over inventory, returns item to inventory.
	/// </summary>
	void OnMouseUp(){
		followMouse = false;
		
//		Vector3 p = this.transform.position;
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (trash.Raycast(ray, out hit, 100.0F)){
			print ("Trash item");
			Destroy (this.gameObject);
		}

//		p = Camera.main.WorldToScreenPoint(p);
//		p.y += 30;
		
//		if(Ease.IsPointWithinRect(p, inventoryButton)){
//			print ("Put item away");
//			inventoryController.TakeItem(item);
//			Destroy (this.gameObject);
//		}
	}

	void Update(){
		if(followMouse){
			Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newPos.z = this.transform.position.z;
			newPos.y -= 6.5f;
			this.transform.position = newPos;
			GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}
		Vector3 pos = this.transform.position;
		if(pos.y < -1.19f) pos.y = -1.19f;
		this.transform.position = pos;
	}
}
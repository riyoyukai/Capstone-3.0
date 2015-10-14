using UnityEngine;
using System.Collections;

public enum ItemType{
	Food,
	Toy
}

public class Item {
	private string path = "Images/";
	public string name;
	public ItemType type = ItemType.Food; // TODO: undefault this from testing
	public string textureName;
	public bool inInventory = true;

	public Item(string itemName){
		name = itemName;
		textureName = path;
		switch(name){
		case "Toy":
			textureName += "lightswitch1";
			break;

		case "Food":
			textureName += "lightswitch1";
			break;
		}
	}
}
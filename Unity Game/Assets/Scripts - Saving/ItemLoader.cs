using UnityEngine;
using System.Collections;

public class ItemLoader : MonoBehaviour {
	
	public GameObject itemHolder;
	public GameObject itemPrefab;

	// Use this for initialization
	void Start () {
		// TODO: load and position items

		GameObject newItem = Instantiate(
			itemPrefab,
			itemPrefab.transform.position,
			itemPrefab.transform.rotation
			) as GameObject;
		newItem.transform.SetParent(itemHolder.transform, false);
		ItemBehavior ib = newItem.GetComponent<ItemBehavior>();
		ib.item = new Item("Food");
		ib.item.itemBehavior = ib;
		GameData.items.Add(ib.item);
	}
}

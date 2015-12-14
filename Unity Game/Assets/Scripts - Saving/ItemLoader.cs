using UnityEngine;
using System.Collections;

public class ItemLoader : MonoBehaviour {

	public GameObject itemPrefab;

	void Start () {		
		if(GameData.items.Count > 0){
			print ("loaded data");
			foreach(Item i in GameData.items){

				GameObject newItem = Instantiate(
					itemPrefab,
					i.position,
					itemPrefab.transform.rotation
					) as GameObject;
				newItem.transform.SetParent(this.transform, false);
				newItem.transform.position = i.position;
				ItemBehavior ib = newItem.GetComponent<ItemBehavior>();
				ib.item = i;
				ib.item.itemBehavior = ib;
			}
		}
	}
}

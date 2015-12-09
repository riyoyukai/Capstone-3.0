using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ProfileLoader : MonoBehaviour {

	public GameObject monsterModel;
	public GameObject eggModel;

	public Text name;
	public Text birthday;
	public Text age;
	public Text species;
	public Text currentLevel;
	public Text nextLevel;
	public RectTransform xpBar;

	Monster monster;

	void Start () {
		monster = GameData.activeMonster;
		species.text = monster.species;
		if (monster.hatched) {
			eggModel.gameObject.SetActive(false);
			name.text = monster.name;
			birthday.text = "" + monster.birthday.ToString("MMMM dd, yyyy") +
							"\nat " + monster.birthday.ToString("h:mm tt");
			int days = (DateTime.Now - monster.birthday).Days;
			age.text = "" + days + " days old";
			currentLevel.text = "" + monster.level;
			nextLevel.text = "" + (monster.level+1);
			float xpScale = (float)monster.xp / (float)monster.xpToNextLevel;
			Vector3 ls = xpBar.localScale;
			ls.x = xpScale;
			xpBar.localScale = ls;
		} else {
			monsterModel.gameObject.SetActive(false);
			birthday.text = "Not hatched!";
			age.text = "???";
			currentLevel.text = "?";
			nextLevel.text = "?";
		}
	}
}

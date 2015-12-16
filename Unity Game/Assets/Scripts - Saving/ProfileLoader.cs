using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ProfileLoader : MonoBehaviour {

	public GameObject monsterModel;
	public GameObject eggModel;

	public Text monsterName;
	public Text birthday;
	public Text age;
	public Text species;
	public Text currentLevel;
	public Text nextLevel;
	public RectTransform xpBar;
	public Image[] hearts;
	public Sprite emptyHeart;
	public Sprite halfHeart;
	public Sprite fullHeart;

	Monster monster;

	void Start () {
		monster = GameData.activeMonster;
		species.text = monster.species;
		if (monster.hatched) {
			eggModel.gameObject.SetActive(false);
			monsterName.text = monster.name;
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
			float care = (float)monster.care;
			int whichCare = (int)(Math.Ceiling(care/2) - 1);
			for(int i = 0; i <= whichCare; i++){
				if(i == whichCare && care % 2 == 1){
					hearts[i].sprite = halfHeart;
				}else{
					hearts[i].sprite = fullHeart;
				}
			}
		} else {
			monsterModel.gameObject.SetActive(false);
			birthday.text = "Not hatched!";
			age.text = "???";
			currentLevel.text = "?";
			nextLevel.text = "?";
		}
	}
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Monster {
	private float hunger = 100; // percent
	private bool hungry = false;
	public int xp = 0;
	public int level = 1;
	public int xpToNextLevel = 100;
	private int totalXP;
	public string name;
	public MonsterBehavior monsterController;
	public bool hatched = false;
	public DateTime birthday;
	public string species = "Sprinkle";

	//private int sleepThisLong = 8; // hours
	//private float sleepiness = 100; // percent
	//private bool sleepy = false;
	//private bool asleep = false;

	public Monster(){

	}

	public void Update(){
		hunger -= Time.deltaTime;
		//UIController.instance.UpdatePetStats(hunger);
		if(!hungry && hunger/100 < .3){
			hungry = true;
//			UIController.instance.Alert("I'm hungry!");
			if(monsterController != null) monsterController.BeginAnimFindFood();
		}
	}

	public void EatFood(){
		hunger = 100;
		hungry = false;
//		UIController.instance.Alert("Yumm!!");
	}

	public void AddXP(int xpToAdd){
		if (xp + xpToAdd > xpToNextLevel) {
			level++;
			xp = xp + xpToAdd - xpToNextLevel;
			xpToNextLevel = level * 100;
		} else {
			xp += xpToAdd;
		}
	}
}

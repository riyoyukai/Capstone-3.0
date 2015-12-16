using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Monster {
	public float hunger = 100; // percent
	public float hungerThreshold = 33;
	public bool hungry = false;
	public int xp = 0;
	public int level = 1;
	public int xpToNextLevel = 100;
	private int totalXP;
	public string name;
	public MonsterBehavior monsterController;
	public bool hatched = false;
	public DateTime birthday;
	public string species = "Sprinkle";
	public float hatchTime;
	public float minHatchMinutes = 5;
	public float maxHatchMinutes = 10;
	public Vector3 position;
	public int care = 0;

	//private int sleepThisLong = 8; // hours
	//private float sleepiness = 100; // percent
	//private bool sleepy = false;
	//private bool asleep = false;

	public Monster(){
		hatchTime = Ease.RandomFloat(minHatchMinutes * 60, maxHatchMinutes * 60);
		Debug.Log("I will hatch in " + hatchTime + " minutes!");
	}

	public void Update(){
		hunger -= (Time.deltaTime / 3); // TODO: this is 5 minutes ish right
		//UIController.instance.UpdatePetStats(hunger);
		if(!hungry && Hungry()){
//			UIController.instance.Alert("I'm hungry!");
			hungry = true;
			if(monsterController != null) monsterController.BeginAnimFindFood(true);
		}
	}

	public bool Hungry(){
		return hunger < hungerThreshold;
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

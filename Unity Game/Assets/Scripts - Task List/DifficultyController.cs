using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyController : MonoBehaviour {

	public Button[] stars;
	public Sprite starEmpty;
	public Sprite starFilled;
	public TaskList taskList;
	public OptionsMenu optionsMenu;

	public void SetDifficult(int dif){
		for(int i = 0; i < stars.Length; i++){
			if(i <= dif){
				stars[i].image.sprite = starFilled;
			}else{
				stars[i].image.sprite = starEmpty;
			}
		}
		optionsMenu.task.difficulty = dif;
	}
}

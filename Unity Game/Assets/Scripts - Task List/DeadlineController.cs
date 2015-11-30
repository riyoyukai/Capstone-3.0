using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadlineController : MonoBehaviour {

	public Text monthText;
	public InputField dayText;
	public InputField yearText;
	public InputField hourText;
	public InputField minuteText;
	public Text ampmText;
	public OptionsMenu optionsMenu;

	void Start(){
		optionsMenu.month = System.DateTime.Today.Month;
		SetMonthText();
	}

	public void NextMonth(){
		optionsMenu.month++;
		if(optionsMenu.month > 12) optionsMenu.month = 1;
		SetMonthText();
	}

	public void PreviousMonth(){
		optionsMenu.month--;
		if(optionsMenu.month < 1) optionsMenu.month = 12;
		SetMonthText();
	}

	private void SetMonthText(){
		string monthName = new System.DateTime(2000, optionsMenu.month, 25).ToString("MMMM");
		monthText.text = monthName;
		ValidateDay();
	}
	
	public void NextDay(){
		optionsMenu.day++;
		int daysInMo = System.DateTime.DaysInMonth (optionsMenu.year, optionsMenu.month);
		if (optionsMenu.day > daysInMo) {
			optionsMenu.day = 1;
		}
		ValidateDay();
	}
	
	public void PreviousDay(){
		optionsMenu.day--;
		int daysInMo = System.DateTime.DaysInMonth (optionsMenu.year, optionsMenu.month);
		if (optionsMenu.day < 1) {
			optionsMenu.day = daysInMo;
		}
		ValidateDay();
	}

	private void SetDayText(){
		dayText.text = "" + optionsMenu.day;
	}
	
	public void NextYear(){
		optionsMenu.year++;
		SetYearText();
	}
	
	public void PreviousYear(){
		optionsMenu.year--;
		if(optionsMenu.year < System.DateTime.Today.Year){
			optionsMenu.year = System.DateTime.Today.Year;
		}
		SetYearText();
	}

	private void SetYearText(){
		yearText.text = "" + optionsMenu.year;
		ValidateDay();
	}
	
	public void NextHour(){
		int tempHour = int.Parse(hourText.text);
		tempHour++;
		if(tempHour > 12){
			tempHour = 1;
		}
		SetHourText(tempHour);
	}
	
	public void PreviousHour(){
		int tempHour = int.Parse(hourText.text);
		tempHour--;
		if(tempHour < 1){
			tempHour = 12;
		}
		SetHourText(tempHour);
	}

	private void SetHourText(int tempHour){
		hourText.text = "" + tempHour;
		if(optionsMenu.ampm == "AM"){
			if(tempHour == 12) tempHour = 0;
		}else if(tempHour != 12){
			tempHour = tempHour + 12;
		}
		optionsMenu.hour = tempHour;
	}
	
	public void NextMinute(){
		optionsMenu.minute++;
		if(optionsMenu.minute > 59){
			optionsMenu.minute = 0;
		}
		SetMinuteText();
	}
	
	public void PreviousMinute(){
		optionsMenu.minute--;
		if(optionsMenu.minute <0){
			optionsMenu.minute = 59;
		}
		SetMinuteText();
	}
	
	private void SetMinuteText(){
		if(optionsMenu.minute < 10) minuteText.text = "0" + optionsMenu.minute;
		else minuteText.text = "" + optionsMenu.minute;
	}

	public void ToggleAMPM(){
		if(optionsMenu.ampm == "AM") optionsMenu.ampm = "PM";
		else optionsMenu.ampm = "AM";
		ampmText.text = optionsMenu.ampm;
	}

	private void ValidateDay(){
		int daysInMo = System.DateTime.DaysInMonth (optionsMenu.year, optionsMenu.month);
		if (optionsMenu.day > daysInMo) {
			optionsMenu.day = daysInMo;
		}
		SetDayText();
	}
}

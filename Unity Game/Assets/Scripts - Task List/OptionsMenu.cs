using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
	
	public int month;
	public int day;
	public int year;
	public int hour;
	public int minute;
	public string ampm;
	
	public Text taskName;

	public InputField hourField;
	public InputField minuteField;
	public InputField dayField;
	public InputField yearField;
	public Text ampmText;
	public Text monthText;
	
	public int remindMinutes;
	public int remindHours;
	public int remindDays;
	public int remindWeeks;
	public int remindMonths;

	public InputField newTaskName;

	public Task task;
	public TaskPanel taskPanel;
	public SubtaskPanel subTaskPanel;

	float errorTimer = 0;
	public Text errorText;

	/********8***/

	public Button[] stars;
	public Sprite starEmpty;
	public Sprite starFilled;
	public TaskList taskList;
	
	public void SetDifficulty(int dif){
		for(int i = 0; i < stars.Length; i++){
			if(i <= dif){
				stars[i].image.sprite = starFilled;
			}else{
				stars[i].image.sprite = starEmpty;
			}
		}
		task.difficulty = dif;
	}

	/************/
	
	public void NextMonth(){
		month++;
		if(month > 12) month = 1;
		SetMonthText();
	}
	
	public void PreviousMonth(){
		month--;
		if(month < 1) month = 12;
		SetMonthText();
	}
	
	private void SetMonthText(){
		string monthName = new System.DateTime(2000, month, 25).ToString("MMMM");
		monthText.text = monthName;
		ValidateDay();
	}
	
	public void NextDay(){
		day++;
		int daysInMo = System.DateTime.DaysInMonth (year, month);
		if (day > daysInMo) {
			day = 1;
		}
		ValidateDay();
	}
	
	public void PreviousDay(){
		day--;
		int daysInMo = System.DateTime.DaysInMonth (year, month);
		if (day < 1) {
			day = daysInMo;
		}
		ValidateDay();
	}
	
	private void SetDayText(){
		dayField.text = "" + day;
	}
	
	public void NextYear(){
		year++;
		SetYearText();
	}
	
	public void PreviousYear(){
		year--;
		if(year < System.DateTime.Today.Year){
			year = System.DateTime.Today.Year;
		}
		SetYearText();
	}
	
	private void SetYearText(){
		yearField.text = "" + year;
		ValidateDay();
	}

	public void ValidateHour(int toAdd){
		int tempHour = int.Parse(hourField.text);
		tempHour += toAdd;
		
		if(tempHour > 12){
			tempHour = 1;
		}
		if(tempHour < 1){
			tempHour = 12;
		}


		if (tempHour == 12 && ampm == "AM"){
			hour = 0;
		}
		if (ampm == "PM" && tempHour != 12) {
			hour = tempHour + 12;
		}

		SetHourText(tempHour);
	}
	
	public void NextHour(){
		ValidateHour(1);

	}
	
	public void PreviousHour(){
		ValidateHour(-1);
	}
	
	private void SetHourText(int tempHour){
		hourField.text = "" + tempHour;
		if(ampm == "AM"){
			if(tempHour == 12) tempHour = 0;
		}else if(tempHour != 12){
			tempHour = tempHour + 12;
		}
		hour = tempHour;
	}
	
	public void NextMinute(){
		minute++;
		if(minute > 59){
			minute = 0;
		}
		SetMinuteText();
	}
	
	public void PreviousMinute(){
		minute--;
		if(minute <0){
			minute = 59;
		}
		SetMinuteText();
	}
	
	private void SetMinuteText(){
		if(minute < 10) minuteField.text = "0" + minute;
		else minuteField.text = "" + minute;
	}
	
	public void ToggleAMPM(){
		if(ampm == "AM") ampm = "PM";
		else ampm = "AM";
		ampmText.text = ampm;

		ValidateHour(0);
	}
	
	private void ValidateDay(){
		int daysInMo = System.DateTime.DaysInMonth (year, month);
		if (day > daysInMo) {
			day = daysInMo;
		}
		SetDayText();
	}

	public void SetDefaultDeadline(){
		day = System.DateTime.Today.Day;
		month = System.DateTime.Today.Month;
		year = System.DateTime.Today.Year;
		hour = System.DateTime.Now.Hour;
		minute = System.DateTime.Now.Minute;
		
		hour++;
		if (hour == 24) {
			hour = 0;
			day++;
			if(day > System.DateTime.DaysInMonth(year, month)){
				day = 1;
				month++;
				if(month == 13){
					month = 1;
					year++;
				}
			}
		}
	}

	public void Open(TaskPanel pTask, SubtaskPanel pSubTask){
		this.gameObject.SetActive(true);
		if(pTask != null){
			task = pTask.task;
			taskPanel = pTask;
		}
		if(pSubTask != null){
			task = pSubTask.task;
			subTaskPanel = pSubTask;
		}
		newTaskName.text = task.name;
		taskName.text = task.name;

		int difficulty = 0;

		DateTime deadline = new DateTime(1, 1, 1);
		if(pTask != null && pTask.task.deadline.Year != 1){
			deadline = pTask.task.deadline;
			difficulty = pTask.task.difficulty;
		}else if(pSubTask != null && pSubTask.task.deadline.Year != 1){
			deadline = pSubTask.task.deadline;
			difficulty = pSubTask.task.difficulty;
		}else{
			SetDefaultDeadline();
		}

		SetDifficulty(difficulty);

		if(deadline.Year != 1){
			hour = deadline.Hour;
			minute = deadline.Minute;
			year = deadline.Year;
			day = deadline.Day;
		}

		hourField.text = "" + hour;
		if(hour == 0) hourField.text = "" + 12;
		if(hour > 12) hourField.text = "" + (hour-12);
		if(minute < 10) minuteField.text = "0" + minute;
		else minuteField.text = "" + minute;
		yearField.text = "" + year;
		dayField.text = "" + day;
		
		ampm = "AM";
		if(hour > 12) ampm = "PM";
		ampmText.text = ampm;

		SetMonthText();
	}

	public void Close(){
		this.gameObject.SetActive(false);
	}
	
	public void E_EnterDay(){
		day = int.Parse(dayField.text);
		int daysInMo = System.DateTime.DaysInMonth (year, month);
		if (day > daysInMo) {
			day = daysInMo;
		}

		dayField.text = "" + day;
	}
	
	public void E_EnterYear(){
		year = int.Parse(yearField.text);
		int daysInMo = System.DateTime.DaysInMonth (year, month);
		if (day > daysInMo) {
			day = daysInMo;
			dayField.text = "" + day;
		}
		if(year < System.DateTime.Today.Year){
			year = System.DateTime.Today.Year;
			yearField.text = "" + year;
		}
	}
	
	public void E_EnterHour(){
		int.TryParse (hourField.text, out hour);
		if(hour > 12){
			hour = 12;
			hourField.text = "" + 12;
		}
		if(hour < 1){
			hour = 1;
			hourField.text = "" + 1;
		}

		if (hour == 12 && ampm == "AM") hour = 0;
		if (ampm == "PM" && hour != 12) {
			hour += 12;
		}
	}
	
	public void E_EnterMinute(){
		int.TryParse (minuteField.text, out minute);
		if(minute > 59){
			minute = 59;
			minuteField.text = "" + 59;
		}
		if(minute < 10) minuteField.text = "0" + minute;
	}
	
	public void E_PreventNegatives(InputField inpField){
		if(inpField.text.Contains("-")){
			inpField.text = inpField.text.Replace("-", "");
		}
	}

	public void E_Confirm(){
		if(newTaskName.text.Trim() != ""){
			task.name = newTaskName.text;
		}
		if(taskPanel != null) taskPanel.taskNameLabel.text = task.name;
		if(subTaskPanel != null) subTaskPanel.subtaskNameLabel.text = task.name;

		DateTime deadlineDate = new DateTime(year, month, day, hour, minute, 0);

		//TODO: build date and compare to today. if in the past, give error.
		if(DateTime.Compare (deadlineDate, DateTime.Now) < 0){
			errorTimer = 3;
			errorText.gameObject.SetActive(true);
		}else{
			task.deadline = deadlineDate;
			this.gameObject.SetActive(false);
			task = null;
			taskPanel = null;
			subTaskPanel = null;
		}
	}

	void Update(){
		if(errorTimer > 0) errorTimer -= Time.deltaTime;
		if(errorTimer < 0){
			errorText.gameObject.SetActive(false);
			errorTimer = 0;
		}
	}
}

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
	public Text deadlineText;

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

	public void Open(TaskPanel pTask, SubtaskPanel pSubTask){
		this.gameObject.SetActive(true);
		task = pTask.task;
		subTaskPanel = pSubTask;
		taskPanel = pTask;
		newTaskName.text = task.name;
		taskName.text = task.name;

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
		
		BuildDeadlineText ();
	}

	public void Close(){
		this.gameObject.SetActive(false);
	}

	private void BuildDeadlineText(){
		int hhInt = hour;
		if(hhInt > 12) hhInt -= 12;
		if(hhInt == 0) hhInt = 12;
		string hh = "" + (hhInt);
		string mm = "" + minute;
		if (minute < 10) mm = "0" + mm;
		string monthString = new System.DateTime(year, month, day).ToString("MMMM");
		deadlineText.text = monthString + " " + day + ", " + year + " at " + hh + ":" + mm + " " + ampm;
	}
	
	public void E_EnterDay(){
		day = int.Parse(dayField.text);
		int daysInMo = System.DateTime.DaysInMonth (year, month);
		if (day > daysInMo) {
			day = daysInMo;
		}
		//TODO: Test to see if days are properly limited

		dayField.text = "" + day;

		BuildDeadlineText ();
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
		BuildDeadlineText ();
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
		if (ampm == "PM") {
			hour += 12;
		}
		BuildDeadlineText ();
	}
	
	public void E_EnterMinute(){
		int.TryParse (minuteField.text, out minute);
		if(minute > 59){
			minute = 59;
			minuteField.text = "" + 59;
		}
		if(minute < 10) minuteField.text = "0" + minute;
		BuildDeadlineText ();
	}
	
	public void E_PreventNegatives(InputField inpField){
		if(inpField.text.Contains("-")){
			inpField.text = inpField.text.Replace("-", "");
		}
	}

	public void E_Confirm(){
		task.name = newTaskName.text;
		if(taskPanel != null) taskPanel.taskNameLabel.text = task.name;
		if(subTaskPanel != null) subTaskPanel.subtaskNameLabel.text = task.name;

		DateTime deadlineDate = new DateTime (year, month, day, hour, minute, 0);
		print ("DEADLINE: " + deadlineDate);

		//TODO: build date and compare to today. if in the past, give error.
		if(DateTime.Compare (deadlineDate, DateTime.Now) < 0){
			// if -1, invalid
		}else{	
			//TODO: give options to task we're editing

			this.gameObject.SetActive(false);
			task = null;
			taskPanel = null;
			subTaskPanel = null;
		}
	}
}

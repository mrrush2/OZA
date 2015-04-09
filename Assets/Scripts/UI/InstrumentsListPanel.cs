using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InstrumentsListPanel : MonoBehaviour 
{
	
	
	public GameObject buttonBase;
	public GameObject thisPanel;
	
	GameObject player;
	InstrumentsOBJ instruments;
	
	GameObject newInstance;
	InstrumentButton info;
	
	int numberOfButtons = 0;
	
	void Awake ()
	{
		
		player = GameObject.FindGameObjectWithTag ("Player"); 	// Init player references
		instruments = player.GetComponent<InstrumentsOBJ>();				// Init scale script ref
	}
	
	void Start ()
	{
		for (int i = 0; i < instruments.instrumentList.Count; i++)
		{
			newInstance = Instantiate (buttonBase) as GameObject;
			newInstance.transform.SetParent(thisPanel.transform, false);
			info = newInstance.GetComponent<InstrumentButton>();
			info.name = instruments.instrumentList[i].name;
			newInstance.name = info.name;
			newInstance.GetComponentInChildren<Text>().text = info.name;
			numberOfButtons++;
		}
	}
	
	void Update ()
	{
		thisPanel.GetComponent<RectTransform>().sizeDelta = new Vector2 (173.1f, (float)((numberOfButtons * 20f) + 1));
		AddInstrumentToMenu();
	}
	
	
	public void AddInstrumentToMenu()
	{
		if (numberOfButtons < instruments.instrumentList.Count)
		{
			for (int i = numberOfButtons; i < instruments.instrumentList.Count; i++)
			{
				newInstance = Instantiate (buttonBase) as GameObject;
				newInstance.transform.SetParent(thisPanel.transform, false);
				info = newInstance.GetComponent<InstrumentButton>();
				info.name = instruments.instrumentList[i].name;
				newInstance.name = info.name;
				newInstance.GetComponentInChildren<Text>().text = info.name;
				numberOfButtons++;
			}
		}
	}
}
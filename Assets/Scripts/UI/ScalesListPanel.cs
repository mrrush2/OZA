using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScalesListPanel : MonoBehaviour 
{

	
	public GameObject buttonBase;
	public GameObject thisPanel;
	RectTransform thisTransform;
	
	GameObject player;
	ScalesOBJ scales;
	
	GameObject newInstance;
	ScaleButton info;
	
	int numberOfButtons = 0;
	
	void Awake ()
	{
		thisTransform = thisPanel.GetComponent<RectTransform>();
		
		player = GameObject.FindGameObjectWithTag ("Player"); 	// Init player references
		scales = player.GetComponent<ScalesOBJ>();				// Init scale script ref
	}

	void Start ()
	{
		for (int i = 0; i < scales.scaleList.Count; i++)
		{
			newInstance = Instantiate (buttonBase) as GameObject;
			newInstance.transform.SetParent(thisPanel.transform, false);
			info = newInstance.GetComponent<ScaleButton>();
			info.name = scales.scaleList[i].name;
			newInstance.name = info.name;
			newInstance.GetComponentInChildren<Text>().text = info.name;
			numberOfButtons++;
		}
	}

	void Update ()
	{
		thisPanel.GetComponent<RectTransform>().sizeDelta = new Vector2 (161.2f, (float)((numberOfButtons * 60f) + 1));
	}
}

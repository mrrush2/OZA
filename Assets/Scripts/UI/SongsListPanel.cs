using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SongsListPanel : MonoBehaviour
{

	
	public GameObject buttonBase;
	public GameObject thisPanel;

	
	GameObject player;
	SongsOBJ songs;
	
	GameObject newInstance;
	SongButton info;
	
	int numberOfButtons = 0;
	
	void Awake ()
	{
		
		player = GameObject.FindGameObjectWithTag ("Player"); 	// Init player references
		songs = player.GetComponent<SongsOBJ>();				// Init scale script ref
	}

	void Start ()
	{
		for (int i = 0; i < songs.comboList.Count; i++)
		{
			if (songs.comboList[i].finalPart)		// Only adds full songs
			{
				newInstance = Instantiate (buttonBase) as GameObject;
				newInstance.transform.SetParent(thisPanel.transform, false);
				info = newInstance.GetComponent<SongButton>();
				info.name = songs.comboList[i].name;
				newInstance.name = info.name;
				newInstance.GetComponentInChildren<Text>().text = info.name;
				numberOfButtons++;
			}
		}
	}

	void Update ()
	{
		thisPanel.GetComponent<RectTransform>().sizeDelta = new Vector2 (173.1f, (float)((numberOfButtons * 20f) + 1));
		AddSongToMenu();
	}
	
	public void AddSongToMenu()
	{
		if (numberOfButtons < songs.comboList.Count)
		{
			for (int i = numberOfButtons; i < songs.comboList.Count; i++)
			{
				newInstance = Instantiate (buttonBase) as GameObject;
				newInstance.transform.SetParent(thisPanel.transform, false);
				info = newInstance.GetComponent<SongButton>();
				info.name = songs.comboList[i].name;
				newInstance.name = info.name;
				newInstance.GetComponentInChildren<Text>().text = info.name;
				numberOfButtons++;
			}
		}
	}
}
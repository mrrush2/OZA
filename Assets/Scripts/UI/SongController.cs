using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SongController : MonoBehaviour 
{
	public GameObject preview;
	public Text description;

	public void SetInfo(SongsOBJ.Combo newSong)
	{
		Sprite previewSprite = Resources.Load<Sprite> ("MenuFrameContents/Song/" + newSong.name);
		string newDesc = newSong.description;
		
		preview.GetComponent<Image>().sprite = previewSprite;
		description.text = newDesc;
	
	
	}

}

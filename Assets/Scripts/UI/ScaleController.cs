using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour 
{

	public Text description;


	public void SetInfo(ScalesOBJ.Scale newScale)
	{
		string newDesc = newScale.description;
		
		description.text = newDesc;
	}
}

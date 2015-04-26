using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LineOfSight : MonoBehaviour {
	//public Transform display;
	//private PolygonCollider2D poly;
	private float posX;
	private float posY;
	public PolygonCollider2D[] colliders;
	public List< Vector2> vertices = new List< Vector2>();
	public LayerMask blocksInSight;
	void Start () 
	{
		posX = transform.position.x;
		posY = transform.position.y;
		colliders = Physics2D.OverlapCircleAll (new Vector2(posX, posY), 3, blocksInSight, -Mathf.Infinity, Mathf.Infinity);
		
	}	
	
	void FixedUpdate () 
	{
		ArrayList Vetices = new ArrayList();
		posX = transform.position.x;
		posY = transform.position.y;
		colliders = Physics2D.OverlapCircleAll (new Vector2 (posX, posY), 3, blocksInSight, -Mathf.Infinity, Mathf.Infinity);
		FindVertices(colliders);
		//allVertices[] = transform.position.z;
		//display.position = allVertices[];
		//Debug.log (colliders);
	}
	
	void FindVertices(PolygonCollider2D[] poly) 
	{
		for(int i = 0; i < poly.GetLength(); i++)
		{ 
			if(poly[i] == PolygonCollider2D)
			{
				Vector2[] temp = poly[i].points;
				for(int j = 0; j < temp.Length; j++)
				{
					vertices.Add( temp[j]);
				}
			}
			else if(poly[i] == BoxCollider2D)
			{

				Vector2[] temp = ;
				for(int j = 0; j < temp.Length; j++)
				{
					vertices.Add( temp[j]);
				}
			}
		}

	}
}

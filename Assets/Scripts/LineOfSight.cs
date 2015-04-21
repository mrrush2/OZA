using UnityEngine;
using System.Collections;



public class LineOfSight : MonoBehaviour {
	public Transform display;
	//private PolygonCollider2D poly;
	private float posX;
	private float posY;
	private PolygonCollider2D[] allColliders;
	public Vector2[] allVertices;
	public LayerMask blocksInSight;
	void Start () 
	{
		posX = transform.position.x;
		posY = transform.position.y;
		allColliders = Physics2D.OverlapCircleAll (Vector2 (posX, posY), 3, blocksInSight, -Mathf.Infinity, Mathf.Infinity);
		
	}
	
	void Update () 
	{
		posX = transform.position.x;
		posY = transform.position.y;
		allColliders = Physics2D.OverlapCircleAll (Vector2 (posX, posY), 3, blocksInSight, -Mathf.Infinity, Mathf.Infinity);
		allVertices = FindVertices(allColliders, posX, posY);
		//allVertices[] = transform.position.z;
		//display.position = allVertices[];
	}
	
	void FindVertices(PolygonCollider2D[] poly, Vector2 pointX, Vector2 pointY) 
	{
		for(int i = 0; i < poly.GetLength(); i++)
		{
			allVertices[] = poly[i].points;
		}

	}
}

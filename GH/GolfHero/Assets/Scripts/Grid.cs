using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour {

	public GameObject GridItem;
	public int colNum = 5;
	public int rowNum = 5;
	public float xSpace = 1.0f;
	public float ySpace = 1.0f;
	private float tempX = 0;
	private float tempY = 0;

	// Use this for initialization
	void Start () {
		createGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void createGrid(){
		//int x = 144;
		//int z = -38;
		//int flag = 0;
		for (int i = -7; i < colNum-7; i++) {
			for (int j = -35; j < rowNum-35; j++) {
				/**if (flag == 1) {
					j++;
				}**/
				//GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Quad); 
				//plane.transform.position = new Vector3 (x, 52, z);
				//plane.transform.eulerAngles = new Vector3 (90f, 0, 0);
				Instantiate(GridItem, new Vector3(i+tempX, 52, j+tempY), Quaternion.identity);
				tempY += ySpace;
				//x++;
				//flag++;
			}
			//z++;
			//x = 144;
			tempX += xSpace;//change the value of seperation between columns
			tempY = 0;
		}
	}
}

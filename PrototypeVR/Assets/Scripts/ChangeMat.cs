using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{

    //in the editor this is what you would set as the object you wan't to change
    //public bool setMaterial_a;
	public Material Material1;
    //in the editor this is what you would set as the object you wan't to change
    public GameObject Object;
    public  bool setMaterial_a;
    // Start is called before the first frame update
	int numOfChildren;
    void Start()
    {
        numOfChildren = Object.transform.childCount;
    }

    // Update is called once per frame
     void OnMouseDown()
     {
		//setMaterial_a = true;
     	 for(int i = 0; i < numOfChildren; i++)
			 {
			     GameObject child = Object.transform.GetChild(i).gameObject;
			     child.GetComponent<Renderer>().material = Material1;
			 }
 	 }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMat : MonoBehaviour
{
     public Material Material1;
     //in the editor this is what you would set as the object you wan't to change
     public GameObject Object;
     public bool amat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void OnMouseDown()
     {
     	if (amat ==true)
	 	 //if(ChangeMat.setMaterial_a == true)
			 {
			 	Object.GetComponent<MeshRenderer> ().material = Material1;
			 }
     	
 	 }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMagicWandTrigger : MonoBehaviour
{

	public MagicWand MagicWand;

	public string ActivateKey = "w";
	public string DeactivateKey = "s";
	
	public void Start()
	{
		
	}

	public void Update()
	{
		//if(Input.GetKeyDown(ActivateKey)) MagicWand.Activate();
		//if(Input.GetKeyDown(DeactivateKey)) MagicWand.Deactivate();

		if (Input.GetAxis("VRTK_Axis10_RightTrigger") > 0.5f && !MagicWand.IsActivated) MagicWand.Activate();
		if(Input.GetAxis("VRTK_Axis10_RightTrigger") < 0.5f && MagicWand.IsActivated) MagicWand.Deactivate();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphCanvas : MonoBehaviour
{

	public MagicWand MagicWand;
	public SpellTarget SpellTarget;
	public float MaxProjectedDistance = 2f;
	public float ProjectionScale = 0.5f;
	
	public void Start()
	{
		
	}

	public void Update()
	{
		transform.LookAt(MagicWand.transform.position, Vector3.up);
	}
}

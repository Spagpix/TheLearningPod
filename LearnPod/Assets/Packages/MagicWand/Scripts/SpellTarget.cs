using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTarget : MonoBehaviour
{

	public MagicWand MagicWand;

	public float ForceAmount = 5f;
	public float DampingAmount = 2f;

	public float ForceFalloffStart = 0.3f;
	public AnimationCurve DistanceCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public bool Activated = false;

    public bool IsTippable;

	private Rigidbody _rigidbody;
	
	public void Activate(MagicWand magicWand)
	{
		MagicWand = magicWand;
		_rigidbody = GetComponent<Rigidbody>();

		MagicWand.ObjectTargetDistance = (MagicWand.transform.position - transform.position).magnitude;

		Activated = true;
	}

	public void Deactivate()
	{
		Activated = false;
	}

	public void FixedUpdate()
	{
		if (!Activated) return;

		var targetPosition = MagicWand.transform.position + MagicWand.transform.forward * MagicWand.ObjectTargetDistance;

		var forceMultiplier = 1f;
        var offsetDistance = (targetPosition - transform.position).magnitude;

        if(IsTippable)
        {
            _rigidbody.rotation = Quaternion.LookRotation((MagicWand.transform.position - transform.position).normalized, Vector3.up);
            _rigidbody.rotation *= Quaternion.Euler(0f, 0f, -MagicWand.transform.eulerAngles.z);
        }

        if(offsetDistance > 2f)
        {
            MagicWand.Deactivate();
        }

		if (offsetDistance < ForceFalloffStart) {
			forceMultiplier = DistanceCurve.Evaluate(offsetDistance / ForceFalloffStart);
		}
		_rigidbody.AddForce((targetPosition - transform.position).normalized * ForceAmount * forceMultiplier * Time.fixedDeltaTime);
		_rigidbody.AddForce(-_rigidbody.velocity * DampingAmount * Time.fixedDeltaTime);
	}
}

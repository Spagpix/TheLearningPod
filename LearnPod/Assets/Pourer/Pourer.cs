using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pourer : MonoBehaviour
{

	public Transform BlobSpawnPoint;

	public Object BlobPrefab;

	public float PositionRandomise = 0.05f;
	public float ScaleRandomise = 0.1f;

	public float MinPourRate = 1f;
	public float MaxPourRate = 10f;

	public float Volume = 1f;
	public int PourCount = 0;
	public int RequiredPourCount = 50;

	public Collider TargetCollider;

	private float _nextSpawnTime;

	public float lerpAmount;

	public UnityEvent OnPourAchieved;
	private bool _pourDone;

 	protected virtual void Update()
    {
	    if (Volume <= 0f) return;
	    
	    var xRot = transform.eulerAngles.x;
	    var zRot = transform.eulerAngles.z;
	    while (xRot > 360f) xRot -= 360f;
	    while (zRot > 360f) zRot -= 360f;
	    while (xRot < 360f) xRot += 360f;
	    while (zRot < 360f) zRot += 360f;
	    
	    //lerpAmount = (Mathf.Max(xRot, zRot) - MinPourAngle) / (MaxPourAngle - MinPourAngle);
	    lerpAmount = Vector3.Dot(transform.up, Vector3.down);
	    if (lerpAmount > 0.01f) {
		    if (Time.time > _nextSpawnTime) {
			    SpawnBlob();

			    _nextSpawnTime += 1f / Mathf.Lerp(MinPourRate, MaxPourRate, lerpAmount);
		    }
	    }
 	}

    public void AddPour(Collider other)
    {
	    if (!TargetCollider.Equals(other)) return;
	    
	    //GetComponent<PourThingFromLukesProject>().Volume -= 0.01f;
	    
	    PourCount++;
	    Volume -= 0.005f;

	    if (PourCount >= RequiredPourCount && !_pourDone) {
		    OnPourAchieved.Invoke();
		    _pourDone = true;
	    }
    }

    private void SpawnBlob()
    {
	    var newObj = Instantiate(BlobPrefab) as GameObject;
	    var blob = newObj.GetComponent<WaterBlob>();
	    blob.MyPourer = this;
	    newObj.transform.position = BlobSpawnPoint.position;
	    newObj.transform.Translate(new Vector3(Random.value, Random.value, Random.value) * PositionRandomise);
	    var scaleAmount = new Vector3(1f + (Random.value - 0.5f) * ScaleRandomise, 1f + (Random.value - 0.5f) * ScaleRandomise, 1f + (Random.value - 0.5f) * ScaleRandomise);

	    newObj.transform.localScale.Scale(scaleAmount);
    }
}

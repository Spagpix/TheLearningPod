using UnityEngine;

public class WaterBlob : MonoBehaviour
{

	public Pourer MyPourer;

    public void OnCollisionEnter(Collision collision)
    {
	    
	    if(MyPourer != null) MyPourer.AddPour(collision.collider);
	    Destroy(gameObject);
    }
}

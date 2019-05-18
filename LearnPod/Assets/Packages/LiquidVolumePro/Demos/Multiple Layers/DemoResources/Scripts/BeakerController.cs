using UnityEngine;

namespace LiquidVolumeFX
{
	public class BeakerController : MonoBehaviour
	{

		[Range (0, 2f)]
		public float rotationSpeed = 1f;



		LiquidVolume lv;

		void Start ()
		{
			lv = GetComponentInChildren<LiquidVolume> ();
		}

		void Update ()
		{
			if (Input.GetKey (KeyCode.W)) {
				transform.Rotate (0, 0, rotationSpeed);
			} else if (Input.GetKey (KeyCode.S)) {
				transform.Rotate (0, 0, -rotationSpeed);
			}

			if (Input.GetKey (KeyCode.Q)) {
				lv.liquidLayers [0].amount += 0.01f;
				lv.UpdateLayers (true);
			}
			if (Input.GetKey (KeyCode.A)) {
				lv.liquidLayers [0].amount -= 0.01f;
				lv.UpdateLayers (true);
			}

			if (Input.GetKeyDown (KeyCode.R)) {
				SetRandomProperties ();
			}
		}


		void SetRandomProperties() {
			int layerCount = lv.liquidLayers.Length;
			if (layerCount == 0)
				return;

			float fillLevel = 0;
			for (int k = 0; k < layerCount; k++) {
				float layerAmount = (1.0f - fillLevel) * Random.value;
				fillLevel += layerAmount;
				lv.liquidLayers [k].amount = layerAmount;
				lv.liquidLayers [k].bubblesOpacity = Random.value;
				lv.liquidLayers [k].color = new Color (Random.value, Random.value, Random.value, Random.value);
				lv.liquidLayers [k].murkColor = new Color (Random.value, Random.value, Random.value, Random.value); 
				lv.liquidLayers [k].murkiness = Random.value;
			}
			// Immediately update layers
			lv.UpdateLayers (true);

		}

	}

}
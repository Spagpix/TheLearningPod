using UnityEngine;
using UnityEngine.Events;

public class KeyboardEventTriggerer : MonoBehaviour
{

	[System.Serializable]
	public class KeyboardEvent
	{
		public string Key;
		public UnityEvent Event;
	}

	public KeyboardEvent[] Events;

	public void Update()
	{
		if(Events == null) return;
		foreach (var e in Events) {
			if (Input.GetKeyDown(e.Key)) e.Event.Invoke();
		}
	}
}

using UnityEngine;
using UnityEngine.Events;

public class IsObjectInsideTrigger : MonoBehaviour
{

    public Collider Trigger;
    public Transform Target;
    
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private bool _inside;

    public void Update()
    {
        if (Trigger.bounds.Contains(Target.position)) {
            if (!_inside) {
                OnEnter.Invoke();
            }

            _inside = true;
        }
        else {
            if (_inside) {
                OnExit.Invoke();
            }

            _inside = false;
        }
    }
}

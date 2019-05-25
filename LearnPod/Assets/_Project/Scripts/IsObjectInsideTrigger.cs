using UnityEngine;
using UnityEngine.Events;

public class IsObjectInsideTrigger : MonoBehaviour
{

    public Collider Trigger;
    public Transform Target;
    
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public bool ClampToGround = false;

    private bool _inside;

    public void Update()
    {
        var pos = Target.position;
        if (ClampToGround) pos.y = 0f;
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

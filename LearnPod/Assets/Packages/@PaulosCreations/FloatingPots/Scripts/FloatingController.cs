using UnityEngine;

public class FloatingController : MonoBehaviour
{
    public AnimationCurve floatingAnimation;
    public float moveSpeed, rotationSpeed;

    private Transform potTF;
    private Vector3 wantedPosition, rotationVector = new Vector3(0,1,0);
    private float wantedHeight, startHeight, evalAnimCurve;

    // Use this for initialization
    void Start()
    {
        potTF = transform;
        wantedPosition = potTF.position;
        startHeight = potTF.position.y;
    }

    // Update is called once per frame
    void Update()
    {
         if (evalAnimCurve < 1)
             evalAnimCurve += moveSpeed * Time.deltaTime;
         else if (evalAnimCurve >= 1)
             evalAnimCurve = 0;

        wantedPosition[1] = startHeight + floatingAnimation.Evaluate(evalAnimCurve);

        potTF.position = wantedPosition;

        if (rotationSpeed != 0)
            potTF.Rotate(rotationVector, Time.deltaTime * rotationSpeed);
    }
}

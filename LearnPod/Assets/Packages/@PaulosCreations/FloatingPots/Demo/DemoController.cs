using UnityEngine;

public class DemoController : MonoBehaviour
{
    public Transform camBaseTF;
    public GameObject lightsObj;

    private bool rotateCam;

    private void Start()
    {
        Debug.LogWarning("R = Rotate Camera, L = Toggle Light");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            lightsObj.SetActive(!lightsObj.activeSelf);

        if (Input.GetKeyDown(KeyCode.R))
        rotateCam = !rotateCam;

        if (rotateCam)
            camBaseTF.Rotate(Vector3.up, Time.deltaTime * -8f);
    }
}

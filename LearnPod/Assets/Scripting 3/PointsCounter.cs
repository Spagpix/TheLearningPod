using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField]
    private int _Points;
    private bool _HasSaidMessage;
    // create a var collector
    //everytime hit space generate 10 points
    // var hasSaidMessage 
    void Start()
    {

    }

    // if points counted is greater than 50 print
    void Update()
    {

        //if _points is greater or equal to 50 print msg And && or || tell it to stop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _Points += 10;
        }

        if (_Points >= 50 && _HasSaidMessage == false)
        {
            Debug.Log("Your Amazing");
            _HasSaidMessage = true;
        }

    }
}

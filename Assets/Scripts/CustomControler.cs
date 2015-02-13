using UnityEngine;
using System.Collections;
using Uniduino;

public class CustomControler : MonoBehaviour {

    protected Arduino arduino;

   // public int[] blink_pin;
    public int Xaxis, Yaxis;
    public int XValue, YValue;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    void Update()
    {

    }
    public virtual int getXValue()
    {
        return XValue;
    }
    public virtual int getYValue()
    {
        return YValue;
    }
    public virtual float getCorrectedYValue()
    {
        return getYValue() * 0.0333f;
    }
    public virtual int getHeight()
    {
        return (int) (arduino.analogRead(1) * 0.01f);
    }
}

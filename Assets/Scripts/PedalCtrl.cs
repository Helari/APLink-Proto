using UnityEngine;
using System.Collections;
using Uniduino;

public class PedalCtrl : CustomControler
{
    public int coeffY = 5;
    void Start()
    {

        arduino = Arduino.global;
        arduino.Log = (s) => Debug.Log("Arduino: " + s);

        arduino.Setup(ConfigurePins);

        ConfigurePins();
    }

    void ConfigurePins()
    {
        Debug.Log("set pin mode");

        arduino.pinMode(0, PinMode.ANALOG);
        arduino.reportAnalog(0, 1);
        arduino.pinMode(1, PinMode.ANALOG);
        arduino.reportAnalog(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        XValue = arduino.analogRead(0) * coeffY;
        YValue = arduino.analogRead(1) * coeffY - 2000;
        if (XValue > 1005)
        {
            XValue = 1005;
        }
        else if (XValue < 5)
        {
            XValue = 5;
        }
        if (YValue > 1005)
        {
            YValue = 1005;
        }
        else if (YValue < 5)
        {
            YValue = 5;
        }
    }
    public override float getCorrectedYValue()
    {
        return getYValue() * 0.02985f;
    }
    public override int getHeight()
    {
        int temp = (int)((arduino.analogRead(1) * coeffY - 2000) * 0.01f);

        if (temp > 10)
        {
            temp = 10;
        }
        else if (temp < 0)
        {
            temp = 0;
        }

        return temp;
    }
}

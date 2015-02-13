using UnityEngine;
using System.Collections;
using Uniduino;

public class SliderCtrl : CustomControler {


    // Use this for initialization
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

        arduino.pinMode(Xaxis, PinMode.ANALOG);
        arduino.reportAnalog(Xaxis, 1);
        arduino.pinMode(Yaxis, PinMode.ANALOG);
        arduino.reportAnalog(Yaxis, 1);
    }

    // Update is called once per frame
    void Update()
    {
        XValue = arduino.analogRead(Xaxis);
        YValue = arduino.analogRead(Yaxis);
    }
    public override int getYValue()
    {
        return XValue;
    }
}

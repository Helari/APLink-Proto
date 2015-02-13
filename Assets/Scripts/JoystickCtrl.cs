using UnityEngine;
using System.Collections;
using Uniduino;

public class JoystickCtrl : CustomControler
{
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
        XValue = arduino.analogRead(0);
        YValue = arduino.analogRead(1);
    }
}

using UnityEngine;
using System.Collections;

public class Instrument : MonoBehaviour {

    public Color m_color;
    public int m_ID;
    public bool m_available = true;
    private int transpose = -4, currentNote=-1;


	// Use this for initialization
	void Start () {
        m_available = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playInstr(int note)
    {
        if (note >= 0 && note != currentNote)
        { // if some key pressed...
            currentNote = note;
            audio.pitch = note;//Mathf.Pow(2, (note + transpose) / 12.0f);
            audio.loop = true;
            audio.Play();
        }
    }
    public void pauseInstr()
    {
        audio.loop = false;
        audio.Stop();
    }
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float m_acceleration;
    public float m_gameOverY;
    public static float distanceTraveled;

    public Transform thisTransform;
    private Vector3 m_startPosition;
    private bool m_playing;
    private CustomControler m_customControler;
    public int m_instrument;
    public Instrument[] m_instrTab;
    public Camera m_cam;
    private ParticleSystem m_particleSystem;

    public enum ControlerType
    {
        Slider,
        Pedal,
        Joystick
    }
    public ControlerType m_controlerType;

    void Awake()
    {
        thisTransform = transform;
        switch (m_controlerType)
        {
            case ControlerType.Slider :
                m_customControler = gameObject.AddComponent<SliderCtrl>();
                break;
            case ControlerType.Pedal:
                m_customControler = gameObject.AddComponent<PedalCtrl>();
                break;
            case ControlerType.Joystick:
                m_customControler = gameObject.AddComponent<JoystickCtrl>();
                break;
        }
    }
	
	// Use this for initialization
	void Start () {
        GameEventManager.TriggerGameStart();
        m_particleSystem = GetComponent<ParticleSystem>();
        m_instrTab = GameObject.FindObjectsOfType<Instrument>();
        
        for (int i=0;i<m_instrTab.Length;i++)
        {
            if (m_instrTab[i].m_available)
            {
                setInstrument(i);
                m_instrTab[i].m_available = false;
                m_particleSystem.startColor = new Color(m_instrTab[i].m_color.r, m_instrTab[i].m_color.g, m_instrTab[i].m_color.b, 0.35f);
                break;
            }
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        m_cam.transform.position = new Vector3(thisTransform.position.x, m_cam.transform.position.y, m_cam.transform.position.z);
        distanceTraveled = transform.localPosition.x;
        if (Input.GetMouseButtonDown(0))
        {
            m_playing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_playing = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            switchInstrument(m_instrument);
        }
        if (m_playing)
        {
            playInstrument(m_instrument, this.transform.position);
        }
        else
        {
            pauseInstrument();
        }
	}
    void FixedUpdate()
    {
        thisTransform.position = new Vector3(thisTransform.position.x + m_acceleration * Time.deltaTime, m_customControler.getCorrectedYValue()-15f, 0f);
    }

    void playInstrument(int _IDinstrument, Vector3 _position)
    {
        Debug.Log(m_customControler.getHeight());
        m_instrTab[m_instrument].playInstr(m_customControler.getHeight());
        m_particleSystem.startColor = m_instrTab[m_instrument].m_color;
    }
    void pauseInstrument()
    {
        m_instrTab[m_instrument].pauseInstr();
        m_particleSystem.startColor = new Color(m_particleSystem.startColor.r, m_particleSystem.startColor.g, m_particleSystem.startColor.b, 0.35f);
    }
    void switchInstrument(int _IDCurrentInstr)
    {
        _IDCurrentInstr = (_IDCurrentInstr < m_instrTab.Length-1) ? _IDCurrentInstr + 1 : 0;

        if (checkInstrumentAvailable(_IDCurrentInstr))
        {
            setInstrument(_IDCurrentInstr);
        }
        else
        {
            switchInstrument(_IDCurrentInstr);
        }
    }
    public void setInstrument(int _IDInstr)
    {
        m_instrTab[m_instrument].m_available = true;
        m_instrument = _IDInstr;
        m_particleSystem.startColor = new Color(m_instrTab[m_instrument].m_color.r, m_instrTab[m_instrument].m_color.g, m_instrTab[m_instrument].m_color.b, 0.35f);
        m_instrTab[m_instrument].m_available = false;
    }
    bool checkInstrumentAvailable(int _IDTargetInstrument)
    {
        if (m_instrTab[_IDTargetInstrument].m_available)
        {
            return true;
        }
        else
        {
            return true; //NO NEED TO HAVE AVAILABILITY PARAMETER
        }
    }
}

using UnityEngine;

public class CamOrbit : MonoBehaviour
{
    private Transform _XForm_Camera;
    private Transform _XForm_Parent;

    private Vector3 _XForm_Pan;
    private Vector3 _XForm_Rotation;
    public float _CameraDistanceDefault = 3000;


    private Vector3 _LocalRotation;
    private float _CameraDistance;
    private Quaternion _Rotation = Quaternion.Euler(0f, 0f, 0f);
    private Vector3 _LocalPan;

    public float MouseSensitivity = 4f;
    public float ScrollSensitivity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;

    public float MouseSensitivityPan = 400f;

    public bool RotationDisabled = true;
    public bool PanDisabled = true;

    void Start()
    {
        _CameraDistance = _CameraDistanceDefault;
        _XForm_Camera = transform;
        _XForm_Parent = transform.parent;
        _XForm_Pan = _XForm_Parent.position;
        _LocalPan = _XForm_Pan; 

        _XForm_Rotation = new Vector3(_XForm_Parent.rotation.x, _XForm_Parent.rotation.y, _XForm_Parent.rotation.z);
        RotationDisabled = true;
    }

    void Update()
    {
        CheckKeyPresses();

        if (!RotationDisabled)
        {
            CamMovements();
        }

        if (!PanDisabled)
        {
            CamPan();
        }

        if (_XForm_Camera.position.z != _CameraDistance)
        {
            _XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(_XForm_Camera.localPosition.z, _CameraDistance, Time.deltaTime * ScrollDampening));
        }

        if (_XForm_Parent.rotation != _Rotation)
        {
            _XForm_Parent.rotation = Quaternion.Lerp(_XForm_Parent.rotation, _Rotation, Time.deltaTime * OrbitDampening);
        }
    }

    private void CheckKeyPresses()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            toggleCamOrbit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            togglePan();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestView();
        }
    }

    private void CamPan()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _XForm_Parent.Translate(-Vector3.left * Input.GetAxis("Mouse X") * MouseSensitivityPan * Mathf.Abs(_CameraDistance)*Time.deltaTime);
                _XForm_Parent.Translate(-Vector3.up * Input.GetAxis("Mouse Y") * MouseSensitivityPan * Mathf.Abs(_CameraDistance)*Time.deltaTime); 
            }
        }
    }

    private void CamMovements()
    {
        if (Input.GetMouseButton(0))
        {
            CamRotation();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            Scroll();
        }
    }

    private void CamRotation()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            _LocalRotation.y += Input.GetAxis("Mouse X") * MouseSensitivity;
            _LocalRotation.x += Input.GetAxis("Mouse Y") * MouseSensitivity;

            _LocalRotation.x = Mathf.Clamp(_LocalRotation.x, -90f, 90f);
        }
        _Rotation = Quaternion.Euler(_LocalRotation.x, _LocalRotation.y, 0);
    }

    private void Scroll()
    {
        float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;

        ScrollAmount *= (_CameraDistance * 0.3f);
        _CameraDistance += ScrollAmount * -1f;
    }

    public void toggleCamOrbit()
    {
        RotationDisabled = !RotationDisabled;
        PanDisabled = true;
    }

    public void togglePan()
    {
        PanDisabled = !PanDisabled;
        RotationDisabled = true;
    }

    public void RestView()
    {
        _CameraDistance = _CameraDistanceDefault;
        _LocalRotation = _XForm_Rotation;
        _Rotation = Quaternion.Euler(_LocalRotation.x, _LocalRotation.y, 0);
        _XForm_Parent.position = _XForm_Pan; 
    }
}
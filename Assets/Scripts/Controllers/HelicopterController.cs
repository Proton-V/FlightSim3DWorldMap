using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelicopterController : MonoBehaviour
{
    public static HelicopterController Instance { get; set; }

    public List<CamView> CamViews;
    private Camera _mainCam;
    private int _camViewNow = 0;

    private NavMeshAgent _nav;
    private Transform _endPoint;

    public State StateNow;

    private void Awake()
    {
        StateNow = State.Idle;
        Instance = this;
        _mainCam = Camera.main;
        _nav = GetComponent<NavMeshAgent>();
    }
    public void Init(float baseOffset,Transform startPoint, Transform endPoint)
    {
        transform.position = startPoint.position;
        _endPoint = endPoint;
        //SetFlightHeight(baseOffset);
        float setHeight = baseOffset + GameManager.Instance.HeightOffset;
        StartCoroutine(SetHeight(true, setHeight));
        SetCamera();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) SetCameraView();
    }


    private void SetCamera()
    {
        _mainCam.transform.SetParent(gameObject.transform);
        SetCameraView();
    }
    private void SetCameraView()
    {
        CamView camView = CamViews[_camViewNow];
        _mainCam.transform.position = camView.Point.position;
        _mainCam.GetComponent<CameraController>().IsControll = camView.IsControll;
        if (camView.ViewName == CamView.CamViewName.Third) _mainCam.transform.LookAt(transform);
        _camViewNow = _camViewNow < CamViews.Count -1 ? +1 : 0;
    }

    private IEnumerator SetHeight(bool startFlight,float height)
    {
        if (!startFlight)
            yield return new WaitForSeconds(GameManager.Instance.HangTime);
        int factor = startFlight ? 1 : -1;
        float difference = height - _nav.baseOffset * factor;
        for(int i = 0; i < difference; i++)
        {
            yield return new WaitForSeconds(0.02f);
            _nav.baseOffset += factor;
        }
        if(startFlight)
            StartFlight(_endPoint);
    }

    private void StartFlight(Transform endPoint)
    {
        StateNow = State.Flight;
        _nav.SetDestination(endPoint.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish" && StateNow == State.Flight)
        {
            StateNow = State.Idle;
            StartCoroutine(SetHeight(false, 0.0f));
        }
    }

    public enum State
    {
        Idle,
        Flight
    }
}

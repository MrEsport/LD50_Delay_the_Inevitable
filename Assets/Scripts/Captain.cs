using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Captain : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnShipHit = new UnityEvent();
    [HideInInspector] public UnityEvent OnShipRepair = new UnityEvent();
    
    #region Singleton
    private static Captain instance;
    public static Captain myCaptain { get => instance; set => instance = value; }
    #endregion

    public Transform boatTransform;
    public Transform aboveWaterPoint;
    public Transform drownPoint;

    public float HullDamageAndRepairValue = .5f;

    private int hullHoles = 3;

    private void Awake()
    {
        if (myCaptain == null)
            myCaptain = this;
    }

    private void Start()
    {
        OnShipHit.AddListener(AddHole);
        OnShipRepair.AddListener(RepairHole);
        Bar.Instance.OnFloodValueChanged.AddListener(LowerShipLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            OnShipHit.Invoke();
        if (Input.GetKeyDown(KeyCode.R))
            OnShipRepair.Invoke();
    }

    private void AddHole()
    {
        hullHoles++;
        Bar.Instance.IncreaseFloodingSpeed(HullDamageAndRepairValue);
    }

    private void RepairHole()
    {
        hullHoles--;
        Bar.Instance.DecreaseFloodingSpeed(HullDamageAndRepairValue);
    }

    private void LowerShipLevel(float percentage)
    {
        boatTransform.position = Vector2.Lerp(aboveWaterPoint.position, drownPoint.position, percentage);
    }

    private void Removelisteners()
    {
        OnShipHit.RemoveListener(AddHole);
        OnShipRepair.RemoveListener(RepairHole);
        Bar.Instance.OnFloodValueChanged.RemoveListener(LowerShipLevel);
    }

    private void OnDestroy() => Removelisteners();
}

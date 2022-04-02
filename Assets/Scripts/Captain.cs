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

    [SerializeField] private float HullDamageAndRepairValue = .5f;
    [SerializeField] private int minHolesNumber = 3;

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

        // Initialize UI
        UIManager.Instance.OnHolesUpdate.Invoke(hullHoles);
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
        AddToHoleNumber(1);
        Bar.Instance.IncreaseFloodingSpeed(HullDamageAndRepairValue);
    }

    private void RepairHole()
    {
        if (hullHoles <= minHolesNumber)
            return;

        AddToHoleNumber(-1);
        Bar.Instance.DecreaseFloodingSpeed(HullDamageAndRepairValue);
    }

    private void AddToHoleNumber(int value)
    {
        hullHoles += value;
        UIManager.Instance.OnHolesUpdate.Invoke(hullHoles);
    }

    private void LowerShipLevel(float percentage)
    {
        Vector2 targetPosition = Vector2.Lerp(aboveWaterPoint.position, drownPoint.position, percentage);

        float easeT = Vector2.Distance(boatTransform.position, targetPosition) >= .02f ? .18f : 1;
        boatTransform.position = Vector2.Lerp(boatTransform.position, targetPosition, easeT);
    }

    private void Removelisteners()
    {
        OnShipHit.RemoveListener(AddHole);
        OnShipRepair.RemoveListener(RepairHole);
        Bar.Instance.OnFloodValueChanged.RemoveListener(LowerShipLevel);
    }

    private void OnDestroy() => Removelisteners();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bar : MonoBehaviour
{
    [HideInInspector] public UnityEvent<float> OnFloodValueChanged = new UnityEvent<float>();

    private static Bar instance = null;
    public static Bar Instance { get => instance; set => instance = value; }


    [Header("Flood Bar")]
    [SerializeField] private float flood = 0f;
    public float FloodLevel
    {
        get => flood;
        set => flood = value;
    }

    private bool isFlooding = false;

    [SerializeField] private float floodingSpeed = 2;
    [SerializeField] private float minFlood = 0f;
    [SerializeField] private float maxFlood = 100f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            IncreaseFloodingSpeed(5f);
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            DecreaseFloodingSpeed(5f);

        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!isFlooding)
                StartFlooding();
            else
                StopFlooding();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
            BucketRemoveFlood(10f);
        if (Input.GetKeyDown(KeyCode.Keypad2))
            BucketAddFlood(10f);
#endif
    }

    private void FixedUpdate()
    {
        if (isFlooding)
            UpdateFloodLevel();
    }

    public void BucketRemoveFlood(float value)
        => FloodLevel -= value;

    public void BucketAddFlood(float value)
        => FloodLevel += value;

    public void IncreaseFloodingSpeed(float value)
        => floodingSpeed += value;

    public void DecreaseFloodingSpeed(float value)
        => floodingSpeed -= value;

    private void StartFlooding() => isFlooding = true;
    private void StopFlooding() => isFlooding = false;

    private void UpdateFloodLevel()
    {
        FloodLevel += floodingSpeed * Time.fixedDeltaTime;
        FloodLevel = Mathf.Clamp(FloodLevel, minFlood, maxFlood);

        OnFloodValueChanged.Invoke(Mathf.RoundToInt(FloodLevel) / maxFlood);
    }
}

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

    [SerializeField] private int floodingSpeed = 2;
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
            IncreaseFloodingSpeed();
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            DecreaseFloodingSpeed();

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

        if (isFlooding)
            UpdateFloodLevel();
    }

    public void BucketRemoveFlood(float value) =>
        FloodLevel -= value;

    public void BucketAddFlood(float value) =>
        FloodLevel += value;

    private void StartFlooding() => isFlooding = true;
    private void StopFlooding() => isFlooding = false;

    private void IncreaseFloodingSpeed() => floodingSpeed += 5;
    private void DecreaseFloodingSpeed() => floodingSpeed -= 5;

    private void UpdateFloodLevel()
    {
        FloodLevel += floodingSpeed * Time.deltaTime;
        FloodLevel = Mathf.Clamp(FloodLevel, minFlood, maxFlood);

        OnFloodValueChanged.Invoke(Mathf.RoundToInt(FloodLevel) / maxFlood);
    }
}

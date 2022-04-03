using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent<float> OnBucketUpdate = new UnityEvent<float>();
    [HideInInspector] public UnityEvent<int> OnPlanksUpdate = new UnityEvent<int>();
    [HideInInspector] public UnityEvent<int> OnHolesUpdate = new UnityEvent<int>();

    public Image bucketEmpty;
    public Image bucketFilled;
    public Image ProgressFill;

    public GameObject planksUI;
    public Text plankCount;

    public Text holesCount;

    #region Singleton
    private static UIManager instance;
    public static UIManager Instance { get => instance; set => instance = value; }
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        OnBucketUpdate.AddListener(UpdateBucketFill);
        OnPlanksUpdate.AddListener(UpdatePlanksCount);
        OnHolesUpdate.AddListener(UpdateHolesCount);
    }

    private void UpdateBucketFill(float value)
    {
        ProgressFill.fillAmount = value;

        if(value <= 0)
        {
            bucketEmpty.enabled = true;
            bucketFilled.enabled = false;
        }
        else if(value >= 1)
        {
            bucketEmpty.enabled = false;
            bucketFilled.enabled = true;
        }
    }

    private void UpdatePlanksCount(int value)
    {
        plankCount.text = value.ToString();
        planksUI.SetActive(value > 0);
    }

    private void UpdateHolesCount(int value)
    {
        holesCount.text = value.ToString();
    }

    private void RemoveListeners()
    {
        OnBucketUpdate.RemoveListener(UpdateBucketFill);
        OnPlanksUpdate.RemoveListener(UpdatePlanksCount);
        OnHolesUpdate.RemoveListener(UpdateHolesCount);
    }

    private void OnDestroy() => RemoveListeners();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToFillLevel : MonoBehaviour
{
    [SerializeField][Range(0f,100f)] private float triggerValue;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private bool doOnce = false;

    private bool doneOnce = false;

    private void Start()
    {
        if (newSprite != null)
            Bar.Instance.OnFloodValueChanged.AddListener(ChangeSprite);
    }

    private void ChangeSprite(float value)
    {
        if (doOnce && doneOnce)
            return;

        if (value >= triggerValue / 100f)
        {
            if (doOnce)
                doneOnce = true;

            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }

    private void RemoveListeners()
    {
        if (newSprite != null)
            Bar.Instance.OnFloodValueChanged.RemoveListener(ChangeSprite);
    }

    private void OnDestroy() => RemoveListeners();
}

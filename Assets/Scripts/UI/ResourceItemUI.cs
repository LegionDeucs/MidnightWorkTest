using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceItemUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text countText;

    internal void SetCount(int count)
    {
        countText.text = count.ToString();
    }

    internal void SetIcon(Sprite itemSprite)
    {
        itemIcon.sprite = itemSprite;
    }
}

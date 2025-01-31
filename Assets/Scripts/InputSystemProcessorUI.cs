using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InputSystemProcessorUI : MonoBehaviour
{
    [SerializeField] private Image ChangeVacuumTypeImage;
    [SerializeField] private Image ChangeItemTypeImage;

    [SerializeField] private Sprite suckingItemsIcon;
    [SerializeField] private Sprite throwingItemsIcon;
    [SerializeField] private Sprite selectSprite;

    private InputSystemProcessor inputSystem;
    private PlayerObserver playerObserver;

    private void Start()
    {
        inputSystem = ServiceLocations.ServiceLocator.Context.GetSingle<InputSystemProcessor>();
        playerObserver = ServiceLocations.ServiceLocator.Context.GetSingle<PlayerObserver>();

        inputSystem.OnChangeVacuumType += InputSystem_OnChangeVacuumType;
        playerObserver.OnThrowingItemChanged += PlayerObserver_OnItemChanged;
        playerObserver.OnSuckingItemChanged += PlayerObserver_OnItemChanged;
    }

    private void PlayerObserver_OnItemChanged(ItemConfig item)
    {
        ChangeItemTypeImage.sprite = item.itemSprite;
    }

    private void InputSystem_OnChangeVacuumType(VacuumType vacuumType)
    {
        ChangeItemTypeImage.sprite = selectSprite;
        switch (vacuumType)
        {
            case VacuumType.Vacuum:
                ChangeVacuumTypeImage.sprite = suckingItemsIcon;
                break;
            case VacuumType.ReverseVacuum:
                ChangeVacuumTypeImage.sprite = throwingItemsIcon;
                break;
        }
    }
}

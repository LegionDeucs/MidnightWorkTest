using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.OnScreen;

public class Joystick : MonoBehaviour
{
    [SerializeField] private bool useFade;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;
    [SerializeField] private AnimationCurve fadeCurve;

    private Coroutine fadeRoutine;
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += Touch_onFingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += Touch_onFingerUp;
    }

    private void Touch_onFingerDown(Finger finger)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        transform.position = finger.screenPosition;

        if(useFade)
            fadeRoutine = StartCoroutine(canvasGroup.DoFade(fadeDuration, 1, fadeCurve));
    }

    private void Touch_onFingerUp(Finger finger)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        if(useFade)
            fadeRoutine = StartCoroutine(canvasGroup.DoFade(fadeDuration, 0, fadeCurve));
    }
    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= Touch_onFingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= Touch_onFingerUp;
    }
}

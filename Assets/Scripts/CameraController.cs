using ServiceLocations;
using UnityEngine;

public class CameraController : MonoBehaviour, IService
{
    [SerializeField] private Transform target;
    [SerializeField] private float staticYPosition = 1;


    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform lowSpeedPosition;
    [SerializeField] private Transform highSpeedPosition;
    [SerializeField] private AnimationCurve cameraReactToSpeedCurve;

    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private float localPositionChangeSpeed = 2;
    [SerializeField] private float localRotationChangeSpeed = 180;

    [Space]
    [SerializeField] private Vector2 minMaxDistance;
    [SerializeField] private Vector2 minMaxPositionLerpStrength;
    [SerializeField] private AnimationCurve positionLerpCurve;

    [Space]
    [SerializeField] private Vector2 minMaxRotationAngle;
    [SerializeField] private Vector2 minMaxRotationLerpStrength;
    [SerializeField] private AnimationCurve rotationLerpCurve;

    public void SetupTarget(Transform target)
    {
        this.target = target;
        targetOldPosition = target.position;
    }

    private Vector3 targetOldPosition;
    void LateUpdate()
    {
        if (target == null)
            return;

        float speed = (target.position - targetOldPosition).magnitude/ Time.deltaTime;

        float distance = (transform.position - target.position.WithY(staticYPosition)).magnitude;
        float angle = Quaternion.Angle(transform.rotation, target.rotation);

        float mainPositionLerpStrength = Mathf.Lerp(minMaxPositionLerpStrength.x, minMaxPositionLerpStrength.y, 
                                positionLerpCurve.Evaluate(Mathf.InverseLerp(minMaxDistance.x, minMaxDistance.y, distance)));

        float rotationLerpStrength = Mathf.Lerp(minMaxRotationLerpStrength.x, minMaxRotationLerpStrength.y,
                                rotationLerpCurve.Evaluate(Mathf.InverseLerp(minMaxRotationAngle.x, minMaxRotationAngle.y, angle)));

        transform.SetPositionAndRotation(
            Vector3.Lerp(transform.position, target.position.WithY(staticYPosition), mainPositionLerpStrength * Time.deltaTime),
            Quaternion.Lerp(transform.rotation, target.rotation, rotationLerpStrength * Time.deltaTime));

        Vector3 targetLocalPosition = Vector3.Lerp(lowSpeedPosition.localPosition, highSpeedPosition.localPosition,
            cameraReactToSpeedCurve.Evaluate(Mathf.InverseLerp(minMaxSpeed.x, minMaxSpeed.y, speed)));

        Quaternion targetLocalRotation = Quaternion.Lerp(lowSpeedPosition.localRotation, highSpeedPosition.localRotation,
            cameraReactToSpeedCurve.Evaluate(Mathf.InverseLerp(minMaxSpeed.x, minMaxSpeed.y, speed)));

        float moveDuraiton = (targetLocalPosition - mainCamera.localPosition).magnitude / localPositionChangeSpeed;
        float rotationDuraiton = Quaternion.Angle(targetLocalRotation, mainCamera.localRotation) / localRotationChangeSpeed;

        float applied = Mathf.InverseLerp(0, moveDuraiton, Time.deltaTime);
        float rotationApplied = Mathf.InverseLerp(0, rotationDuraiton, Time.deltaTime);

        mainCamera.SetLocalPositionAndRotation(Vector3.Lerp(mainCamera.localPosition, targetLocalPosition, applied), 
                                               Quaternion.Lerp(mainCamera.localRotation, targetLocalRotation, applied));
        transform.eulerAngles = transform.eulerAngles.WithZ(0);

        targetOldPosition = target.position;
    }
}

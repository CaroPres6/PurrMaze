using UnityEngine;

public class CameraSuivi : MonoBehaviour
{
    [SerializeField] private Transform cible;
    [SerializeField] private SecousseCamera secousse;
    [SerializeField, Min(0.1f)] private float vitesseSuivi = 5f;
    [SerializeField] private Vector2 limiteMin = new(-8f, -4f);
    [SerializeField] private Vector2 limiteMax = new(8f, 4f);

    private Vector3 positionBase;

    private void Start() => positionBase = transform.position;

    private void LateUpdate()
    {
        if (cible == null) return;

        Vector3 destination = new(
            Mathf.Clamp(cible.position.x, limiteMin.x, limiteMax.x),
            Mathf.Clamp(cible.position.y, limiteMin.y, limiteMax.y),
            positionBase.z
        );

        positionBase = Vector3.Lerp(positionBase, destination, vitesseSuivi * Time.deltaTime);

        Vector3 decalage = secousse != null ? (Vector3)secousse.Decalage : Vector3.zero;
        transform.position = positionBase + decalage;
    }
}
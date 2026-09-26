using System.Collections;
using UnityEngine;

public class SecousseCamera : MonoBehaviour
{
    [SerializeField, Min(0.01f)] private float duree = 0.14f;
    [SerializeField, Min(0.01f)] private float intensite = 0.09f;
    private Coroutine secousseEnCours;
    public Vector2 Decalage { get; private set; }

    public void Declencher()
    {
        if (secousseEnCours != null) StopCoroutine(secousseEnCours);
        secousseEnCours = StartCoroutine(JouerSecousse());
    }

    private IEnumerator JouerSecousse()
    {
        float temps = 0f;
        while (temps < duree)
        {
            float attenuation = 1f - temps / duree;
            Decalage = Random.insideUnitCircle * intensite * attenuation;
            yield return null;
            temps += Time.deltaTime;
        }
        Decalage = Vector2.zero;
        secousseEnCours = null;
    }

    private void OnDisable() => Decalage = Vector2.zero;
}

using UnityEngine;
using UnityEngine.Events;

public class VieEnnemi : MonoBehaviour
{
    [SerializeField] private int vieMax = 20;

    public UnityEvent<int> OnDegatsSubis;
    public UnityEvent OnMort;

    private int currentVie;

    public int CurrentVie => currentVie;
    public bool EstMort => currentVie <= 0;

    private void Awake()
    {
        currentVie = vieMax;
    }

    public void SubirDegats(int degats)
    {
        if (EstMort) return;
        currentVie = Mathf.Max(0, currentVie - degats);
        OnDegatsSubis?.Invoke(currentVie);

        if (currentVie <= 0)
        {
            Mourir();
        }
    }

    private void Mourir()
    {
        OnMort?.Invoke();

        Rigidbody2D corps = GetComponent<Rigidbody2D>();
        if (corps != null)
        {
            corps.linearVelocity = Vector2.zero;
            corps.simulated = false; 
        }

        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if (script != this) script.enabled = false;
        }

        Destroy(gameObject, 1f);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VieJoueur : MonoBehaviour
{
    [SerializeField] private int vieMax = 100;
    [SerializeField] private Slider sliderVie;
    [SerializeField] private Transform pointRespawn; 
    [SerializeField] private float delaiRespawn = 1.5f;

    private int currentVie;
    private bool estMort;
    private Vector3 positionDepart;

    private Animator animator;
    private Rigidbody2D corps;
    private MouvementJoueur mouvement;
    private AttaqueJoueur attaque;

    public int CurrentVie => currentVie;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        corps = GetComponent<Rigidbody2D>();
        mouvement = GetComponent<MouvementJoueur>();
        attaque = GetComponent<AttaqueJoueur>();
        positionDepart = transform.position;
    }

    private void Start()
    {
        currentVie = vieMax;

        if (sliderVie != null)
        {
            sliderVie.maxValue = vieMax;
            sliderVie.value = currentVie;
        }
    }

    public void TakeDamage(int damage)
    {
        if (estMort) return;

        currentVie -= damage;

        if (currentVie < 0)
        {
            currentVie = 0;
        }

        if (sliderVie != null)
        {
            sliderVie.value = currentVie;
        }

        if (currentVie <= 0)
        {
            Mourir();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (estMort) return;

        if (other.CompareTag("ZoneInterdite"))
        {
            currentVie = 0;

            if (sliderVie != null)
            {
                sliderVie.value = 0;
            }

            Mourir();
        }
    }

    private void Mourir()
    {
        estMort = true;

        if (mouvement != null) mouvement.enabled = false;
        if (attaque != null) attaque.enabled = false;
        if (corps != null) corps.linearVelocity = Vector2.zero;

        if (GestionJeu.Instance != null)
        {
            GestionJeu.Instance.PerdreVie();

            if (GestionJeu.Instance.vies > 0)
            {
                StartCoroutine(Reapparaitre());
            }
        }
    }

    private IEnumerator Reapparaitre()
    {
        yield return new WaitForSeconds(delaiRespawn);

        transform.position = pointRespawn != null
            ? pointRespawn.position
            : positionDepart;

        currentVie = vieMax;

        if (sliderVie != null)
        {
            sliderVie.value = currentVie;
        }

        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }

        if (mouvement != null) mouvement.enabled = true;
        if (attaque != null) attaque.enabled = true;

        estMort = false;
    }
}
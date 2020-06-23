using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool _souldBeDead;

    void Start()
    {
        StartCoroutine(LifeTimeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (_souldBeDead == true)
            Despawn();
        else
            return;
    }
    private void Movement()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        _souldBeDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IDamageable hit = collision.GetComponent<IDamageable>();

            if (hit != null)
            {
                hit.Damage();
                Despawn();
            }
        }
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }
}

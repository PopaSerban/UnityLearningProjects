using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    [SerializeField] private GameObject _acidProjectile;

    public override void Init()
    {
        base.Init();

        Health = base.health;
    }
    public override void Update()
    {
        
    }

    public void Damage()
    {
        Health--;
        if (Health < 1)
            //Destroy(this.gameObject);
            _animator.SetTrigger("death");
    }

    public override void WaypointsMovement()
    {
        
    }

    public void Attack()
    {
        Vector2 acidPosition = transform.position;

        Instantiate(_acidProjectile, acidPosition, Quaternion.identity);
    }
}


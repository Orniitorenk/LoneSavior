using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShot : MonoBehaviour
{
    public Transform tankTower; // Tank tower blown off if its dead so assign it avoid any errors
    public GameObject tankBullet, tankBulletUpgraded; // two types of bullets, deals different damages to enemy
    public GameObject tankShotParticleEffects; // particle effect when shooting
    public Transform firingPoint; // bullets instantiated point
    private bool alreadyAttacked;
    public float timeBetweenAttacks = 1f;
    public float upgradedTimeBetweenAttacks = 0.5f;
    public bool upgradedReloadSpeed = false;
    public Quaternion bulletSpawnOffset;
    private GameObject _bulletInstanstiate;
    [SerializeField] private int _totalBullet;
    public Queue<GameObject> _bulletPool = new Queue<GameObject>();
    public BulletScript bulletScript;

    private void Start()
    {
        CreateBulletAtStart();
    }

    

    private void Update()
    {
        FireBullet();
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void CreateBulletAtStart()
    {
        for(int i = 0; i < _totalBullet; i++)
        {
            _bulletInstanstiate = Instantiate(tankBullet);
            _bulletInstanstiate.SetActive(false);
            _bulletPool.Enqueue(_bulletInstanstiate);
        }
    }


    private GameObject GetBulletFromPool()
    {
        foreach(GameObject bullet in _bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.gameObject.GetComponent<TankBullet>().bulletLife = 5f;                          
                return bullet;
            }
            
        }
        return null;
    }





    private void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            _bulletInstanstiate = GetBulletFromPool();                              
            if(_bulletInstanstiate != null)
            {
                if(tankTower != null)
                {
                    if (!alreadyAttacked)
                    {
                        StartCoroutine(ParticleAfter());
                        
                        alreadyAttacked = true; // Put cooldown and start reload speed.
                        if (!upgradedReloadSpeed) // Checks for player upgraded reload speed
                        {
                            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset reload speeds so player can attack again
                        }
                        else
                        {
                            Invoke(nameof(ResetAttack), upgradedTimeBetweenAttacks); // Reset reload speeds so player can attack again
                        }
                    }

                }
                
                
            }
        }
    }

    IEnumerator ParticleAfter()
    {
        _bulletPool.Dequeue();
        _bulletInstanstiate.transform.position = firingPoint.transform.position;
        _bulletInstanstiate.transform.rotation = firingPoint.transform.rotation * bulletSpawnOffset;
        tankBullet.gameObject.GetComponent<TankBullet>().bulletLife = 5f;
        _bulletInstanstiate.SetActive(true);
        new WaitForSeconds(0.2f);
        Instantiate(tankShotParticleEffects, firingPoint.position, tankTower.rotation * Quaternion.Euler(0f, -90f, 0f)); // Shot effect instantiated
        yield return null;
    }

    
}

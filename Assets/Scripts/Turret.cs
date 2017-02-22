using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;

	[Header("General")]
	public float fireRate = 1f;
	

    [Header("Use Bullets (Default)")]
    public GameObject bulletPrefab;
    public float range = 15f;
    private float fireCountdown = 0f;

    [Header("Use Lazer")]
    public bool useLazer = false;
    public LineRenderer lineRenderer;

    [Header("Unity Setup Fields")]
	public Transform partToRotate;
	public float turnRate = 10f;
    public string enemyTag = "Enemy";

	
	public Transform firePoint;


	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance < range)
        {
            target = nearestEnemy.transform;
        }
        else {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null) {
            if (useLazer) {
                if (lineRenderer.enabled) {
                    lineRenderer.enabled = false;
                }
            }
            return;
        }
            

        LockOnTarget();

        if (useLazer) {
            Laser();
        } else {

        }

		if (fireCountdown <= 0f) {
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
		
	}

    void Laser() {
        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }

    void LockOnTarget() {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnRate).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

	void Shoot(){
		GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGo.GetComponent<Bullet>();

		if (bullet != null) {
			bullet.Seek(target);
		}
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed = 70f;
	public float explosionRadius = 0f;
    public int damage = 50;
	private Vector3 latestDir;

	public GameObject impactEffect;


	public void Seek (Transform _target){
		target = _target;
	}
	
	// Update is called once per frame
	void Update () {

		if(target != null){
			latestDir = target.position;
			Vector3 dir = target.position - transform.position;

			float distanceThisFrame = speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame) {
				HitTarget();
				return;
			}

			transform.Translate (dir.normalized * distanceThisFrame, Space.World);

		} else {

			if(latestDir == Vector3.zero){
				Destroy(gameObject);
			}

			float distanceThisFrame = speed * Time.deltaTime;

			Vector3 dir = latestDir - transform.position;

			if (dir.magnitude <= distanceThisFrame) {
				HitTarget();
				return;
			}

			transform.Translate (dir.normalized * distanceThisFrame, Space.World);

		}

		transform.LookAt (target);

	}

	void HitTarget(){
		GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectInstance, 2f);

		if (explosionRadius > 0f) {
			Explode();
		} else {
			Damage(target);
		}

		Destroy (gameObject);
	}

	void Explode(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach(Collider collider in colliders) {
			if (collider.tag == "Enemy") {
				Damage (collider.transform);
			}
		}
    }

	void Damage (Transform enemy){
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null) {
            e.TakeDamage(damage);
        }
        
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}

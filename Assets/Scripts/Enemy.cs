using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;

    public int health = 100;

    public int value = 10;

    public GameObject deathEffect;

	private Transform target;

	private int waypointIndex = 0;

	void Start () {
		target = Waypoints.points[0];
	}

    public void TakeDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        PlayerStats.Money += value;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

	void Update () {
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, target.position) <= 0.5f) {
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint() {
		if (waypointIndex >= Waypoints.points.Length - 1) {
            EndPath();
			return;
		}

		waypointIndex++;
		target = Waypoints.points[waypointIndex];
	}

    void EndPath() {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}

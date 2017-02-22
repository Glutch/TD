using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;

	private Color defaultColor;

	[Header("Optional")]
	public GameObject turret;

	public Vector3 positionOffset;

	private Renderer rend;

	BuildManager buildManager;

	void Start(){
		rend = GetComponent<Renderer> ();
		defaultColor = rend.material.color;
		buildManager = BuildManager.instance;
		rend.enabled = false;
	}

	void OnMouseDown() {
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		if (!buildManager.CanBuild)
			return;


		if(turret != null){
			Debug.Log ("Already turret bro");
			return;
		}

		buildManager.BuildTurretOn(this);		
	}

	public Vector3 GetBuildPosition(){
		return transform.position + positionOffset;
	}

	void OnMouseEnter() {
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

        if (!buildManager.HasMoney) {
            rend.material.color = Color.red;
        }

		if (!buildManager.CanBuild)
			return;

		rend.enabled = true;
		
		//rend.material.color = hoverColor;
	}

	void OnMouseExit() {
		rend.enabled = false;
		rend.material.color = defaultColor;
	}
}

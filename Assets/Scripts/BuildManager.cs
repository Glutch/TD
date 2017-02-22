using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

    public Text moneyText;

    private TurretBlueprint turretToBuild;

    void Update(){
        moneyText.text = "MONEY: " + Mathf.Round(PlayerStats.Money);
    }


	void Awake(){
		if (instance != null) {
			Debug.LogError ("More than one BuildError dafuq");
			return;
		}
		instance = this;
	}

	

	public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node){
        if (PlayerStats.Money < turretToBuild.cost) {
            Debug.Log("Not enough gold");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

		GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

        Debug.Log("Money left: " + PlayerStats.Money);
	}

	public void SelectTurretToBuild(TurretBlueprint turret){
		turretToBuild = turret;
	}
}

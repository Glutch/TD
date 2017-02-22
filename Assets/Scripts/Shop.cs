using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint lazerTower;

    BuildManager buildManager;

    void Start() {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret() {
        Debug.Log("Standard turret selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileTurret()
    {
        Debug.Log("Missile turret selected");
        buildManager.SelectTurretToBuild(missileTurret);
    }

    public void SelectLazerTower()
    {
        Debug.Log("LlazerTower selected");
        buildManager.SelectTurretToBuild(lazerTower);
    }
}
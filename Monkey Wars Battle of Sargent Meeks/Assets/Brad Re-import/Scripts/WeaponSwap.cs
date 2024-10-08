using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    private bool gameIsStart;

    private Weapon currentWeapon;



    public bool weapon2;
    public bool weapon3;
    public bool weapon4;
    public Weapon mfour;
    public Weapon pistol;
    public Weapon cluster;
    public Weapon railgun;
    public GameObject mfourModel;
    public GameObject pistolModel;
    public GameObject clusterModel;
    public GameObject railgunModel;
    private MeshRenderer mfourMesh;
    private MeshRenderer pistolMesh;
    private MeshRenderer clusterMesh;
    private MeshRenderer railgunMesh;

    // Start is called before the first frame update
    void Start()
    {
        gameIsStart = false;
        currentWeapon = mfour;
        weapon2 = false;
        weapon3 = false;
        weapon4 = false;
        mfourMesh = mfourModel.GetComponent<MeshRenderer>();
        pistolMesh = pistolModel.GetComponent<MeshRenderer>();
        clusterMesh = clusterModel.GetComponent<MeshRenderer>();
        railgunMesh = railgunModel.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") && !gameIsStart)
        {
            mfourMesh.enabled = true;
            gameIsStart = true;
        }
        if (Input.GetKeyDown("1"))
        {
            mfourMesh.enabled = true;
            mfour.playerWeapon = true;
            currentWeapon = mfour;
            mfour.showCurrentAmmo = true;
            mfour.showCrosshair = true;
            pistolMesh.enabled = false;
            pistol.playerWeapon = false;
            pistol.showCurrentAmmo = false;
            pistol.showCrosshair = false;
            clusterMesh.enabled = false;
            cluster.playerWeapon = false;
            cluster.showCurrentAmmo = false;
            cluster.showCrosshair = false;
            railgunMesh.enabled = false;
            railgun.playerWeapon = false;
            railgun.showCurrentAmmo = false;
            railgun.showCrosshair = false;
        }
        if (Input.GetKeyDown("2"))
        {
            if (weapon2)
            {
                mfourMesh.enabled = false;
                mfour.playerWeapon = false;
                currentWeapon = pistol;
                mfour.showCurrentAmmo = false;
                mfour.showCrosshair = false;
                pistolMesh.enabled = true;
                pistol.playerWeapon = true;
                pistol.showCurrentAmmo = true;
                pistol.showCrosshair = true;
                clusterMesh.enabled = false;
                cluster.playerWeapon = false;
                cluster.showCurrentAmmo = false;
                cluster.showCrosshair = false;
                railgunMesh.enabled = false;
                railgun.playerWeapon = false;
                railgun.showCurrentAmmo = false;
                railgun.showCrosshair = false;
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (weapon3)
            {
                mfourMesh.enabled = false;
                mfour.playerWeapon = false;
                currentWeapon = cluster;
                mfour.showCurrentAmmo = false;
                mfour.showCrosshair = false;
                pistolMesh.enabled = false;
                pistol.playerWeapon = false;
                pistol.showCurrentAmmo = false;
                pistol.showCrosshair = false;
                clusterMesh.enabled = true;
                cluster.playerWeapon = true;
                cluster.showCurrentAmmo = true;
                cluster.showCrosshair = true;
                railgunMesh.enabled = false;
                railgun.playerWeapon = false;
                railgun.showCurrentAmmo = false;
                railgun.showCrosshair = false;
            }
        }
        if (Input.GetKeyDown("4"))
        {
            if (weapon4)
            {
                mfourMesh.enabled = false;
                mfour.playerWeapon = false;
                currentWeapon = railgun;
                mfour.showCurrentAmmo = false;
                mfour.showCrosshair = false;
                pistolMesh.enabled = false;
                pistol.playerWeapon = false;
                pistol.showCurrentAmmo = false;
                pistol.showCrosshair = false;
                clusterMesh.enabled = false;
                cluster.playerWeapon = false;
                cluster.showCurrentAmmo = false;
                cluster.showCrosshair = false;
                railgunMesh.enabled = true;
                railgun.playerWeapon = true;
                railgun.showCurrentAmmo = true;
                railgun.showCrosshair = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SimulatorController : MonoBehaviour
{
    public static SimulatorController Instance { get; set; }

    private float _maxObtacleSizeY = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateMenu()
    {
        GameManager gm = GameManager.Instance;
        GameObject menuParent = gm.CanvasMenu.Menu.gameObject;
        GameObject butRoutPref = gm.CanvasMenu.ButRoutPrefab;
        int butNum = 0;

        foreach (Rout i in gm.Routs)
        {
            butNum++;
            Button newBut = Instantiate(butRoutPref, menuParent.transform).GetComponent<Button>();
            newBut.GetComponentInChildren<Text>().text = $"Rout {butNum}";
            newBut.onClick.AddListener(()=>StartGame(i));
        }
    }

    public void StartGame(Rout rout)
    {
        if (HelicopterController.Instance == null)
            SpawnHelicopter(rout);
        GameManager.Instance.CanvasMenu.GetComponent<Canvas>().enabled = false;
    }

    public void CreateObtacle(GameObject obj)
    {
        NavMeshObstacle newObtacle = obj.AddComponent<NavMeshObstacle>();
        GameObject newChildObj = new GameObject("Obtacle");
        newChildObj.transform.eulerAngles += new Vector3(0, 30, 0);
        newChildObj.transform.SetParent(obj.transform);
        newChildObj.transform.localPosition = Vector3.zero;
        NavMeshObstacle newChildObtacle = newChildObj.AddComponent<NavMeshObstacle>();
        newChildObtacle.size = newObtacle.size;
        newChildObtacle.center = newObtacle.center;
        newChildObtacle.carving = true;
        Destroy(newObtacle);

        if (_maxObtacleSizeY < newChildObtacle.size.y) _maxObtacleSizeY = newChildObtacle.size.y;
    }

    public void SpawnHelicopter(Rout rout)
    {
        GameManager gm = GameManager.Instance;
        HelicopterController helicopter = Instantiate(gm.HelicopterPrefab).GetComponent<HelicopterController>();
        helicopter.Init(_maxObtacleSizeY, rout.StartPoint, rout.EndPoint);
    }
}

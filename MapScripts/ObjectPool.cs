using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> poolGameObjects 
        = new Dictionary<string, List<GameObject>>()
        {
            {"gaba", new List<GameObject>()},
            {"kati", new List<GameObject>()},
            {"pinti", new List<GameObject>()},
            {"poket", new List<GameObject>()},
            {"sropa", new List<GameObject>()}
        };

    private GameObject gaba;
    private GameObject kati;
    private GameObject pinti;
    private GameObject poket;
    private GameObject sropa;

    void Awake()
    {
        Debug.Log("Awake");
        //スコア2倍
        gaba = (GameObject)Resources.Load("HoldGaba");
        //スコア4倍
        kati = (GameObject)Resources.Load("HoldKati");
        //体力を回復
        pinti = (GameObject)Resources.Load("HoldPinti");
        //ダメージを与える
        poket = (GameObject)Resources.Load("HoldPoket");
        //時間を進める
        sropa = (GameObject)Resources.Load("HoldSropa");
    }

    void Start()
    {
        Debug.Log("Start");
    }

    /**
    引数で受け取ったホールドの種類に該当するオブジェクトを返す

    オブジェクトプールから指定のホールドの種類で非アクティブなものがあればリサイクル
    なければ新しく生成してオブジェクトプールに追加する
    */
    public GameObject GetGameObject(string holdTag){
        string key = holdTag;

        List<GameObject> holdObjects = poolGameObjects[key];

        foreach(GameObject hold in holdObjects){
            if(hold.activeInHierarchy == false){
                hold.SetActive(true);
                return hold;
            }
        }

        GameObject newObj = (GameObject)Instantiate(GetPrefab(holdTag));
        newObj.transform.parent = this.transform;
        poolGameObjects[holdTag].Add(newObj);
        
        return newObj;
    }

    /**
    指定したホールドオブジェクトのアクティビティをfalseに変更
    */
    public void ReleaseGameObject(GameObject hold){
        hold.SetActive(false);
    }

    /**
    ホールドの種類名からプレハブを取得
    */
    private GameObject GetPrefab(string holdTag){
        GameObject hold = null;

        switch(holdTag){
            case "gaba":
                hold = gaba;
                break;
            case "kati":
                hold = kati;
                break;
            case "pinti":
                hold = pinti;
                break;
            case "poket":
                hold = poket;
                break;
            case "sropa":
                hold = sropa;
                break;
        }

        return hold;
    }

    public void SetNonActiveHolds(int threshold, int end){
        List<string> tags = new List<string>(){
            "sropa",
            "poket",
            "pinti",
            "kati",
            "gaba"
        };
        int releasedNum = 0;

        foreach(string tag in tags){
            foreach(GameObject obj in poolGameObjects[tag]){
                float height = obj.transform.position.y;

                if(height < (float)threshold+0.5f){
                    ReleaseGameObject(obj);
                    releasedNum++;
                }
            }
        }
    }

    public int GetCountOfPoolObjects(){
        return poolGameObjects["gaba"].Count+poolGameObjects["kati"].Count+poolGameObjects["pinti"].Count+poolGameObjects["poket"].Count+poolGameObjects["sropa"].Count;
    }
}
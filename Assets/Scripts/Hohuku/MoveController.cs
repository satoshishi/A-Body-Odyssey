using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float distance;
    public float Distance
    {
        private set
        {
            if (value == -1)
                distance = 0f;
            else distance += value;
        }
        get { return distance/ iTween.PathLength(iTweenPath.GetPath(PathName)); }
    }

    private string path_name;
    public string PathName
    {
        set { path_name = value; }
        get { return path_name; }
    }

    private float speed;
    public float Speed
    {
        set { speed = value; }
        get { return speed; }
    }

    public bool IsAdmitMove;

    // Use this for initialization
    void Start()
    {
      //  Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(string p_name = "", bool admit = false,float speed = 0.001f)
    {
        PathName = p_name;
        Distance = -1;
        Speed = speed;
        IsAdmitMove = admit;
        Invoke("Move",0.05f);
    }

    public void Move()
    {
        if (!IsAdmitMove)
            return;

        Debug.Log("move");

        Distance = Speed;
        if (Distance < 0.0f) Distance = 0.0f;
        if (Distance > 1.0f) Distance = 1.0f;
        iTween.PutOnPath(gameObject, iTweenPath.GetPath(PathName), Distance);
        // 少し先の位置(percent+0.01←この数値は任意)を取得(戦略1)
        Vector3 fpos = iTween.PointOnPath(iTweenPath.GetPath(PathName), Distance + 0.01f);
        // 少し先の位置を向かせる(戦略2)
        gameObject.transform.LookAt(fpos);
    }
}

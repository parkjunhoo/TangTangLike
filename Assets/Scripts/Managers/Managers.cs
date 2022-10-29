using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    InputManager _input = new InputManager();
    public static InputManager Input
    {
        get
        {
            return Instance._input;
        }
    }

    
    void Start()
    {
        Init();
    }


    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Managers");
        if (go.Equals(null))
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }
        DontDestroyOnLoad(go);
        _instance = go.GetComponent<Managers>();

    }
}

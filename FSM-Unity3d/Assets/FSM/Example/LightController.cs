using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLToolKits.FSM;
public class LightController : MonoBehaviour
{
    public float MaxIntensity = 1f;
    public float fadeSpeed= 1f;
    private FSMMachine lightFSM;
    private State open;
    private State close;
    private Transition open2Close;
    private Transition close2Open;

    private Light myLight;
    private bool isOpen = true;
    //------灯光忽明忽暗动画参数 Start----
    private bool isAnimation;
    private bool isReset;
    private float target;
    //------End----
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        InitFSM();
    }

    // Update is called once per frame
    void Update()
    {
        lightFSM.UpdateCallback(Time.deltaTime);
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(150f,30f,55f,28f),"Open"))
        {
            isOpen = true;
        }

        if(GUI.Button(new Rect(150f,65f,55f,28f),"Close"))
        {
            isOpen = false;
        }
    }

    /// <summary>
    /// 初始化状态机
    /// </summary>
    private void InitFSM()
    {
        open = new State("Open");
        open.OnEnter +=(IState state)=>{
            myLight.intensity = MaxIntensity;
        };
        open.OnUpdate +=(float f)=>{
            if(isAnimation)
            {
                if(isReset)
                {
                    if(FadeTo(MaxIntensity))
                    {
                        isReset = false;
                        isAnimation = false;
                    }
                }
                else
                {
                    if(FadeTo(target))
                        isReset = true;
                }
            }
            else
            {
                target = Random.Range(0.3f,0.7f);
                isAnimation = true;
            }
        };

        close = new State("Close");
        close.OnEnter +=(IState state)=>{
            myLight.intensity = 0f;
        };

        open2Close = new Transition (open,close);
        open2Close.OnCheck+=()=>
        {
            return !isOpen;
        };
        open2Close.OnTransition+= ()=>
        {
            return FadeTo(0f);
        };
        open.AddTransition(open2Close);
        

        close2Open = new Transition(close,open);
        close2Open.OnCheck+=()=>
        {
            return isOpen;
        };
        close2Open.OnTransition+= ()=>
        {
            return FadeTo(MaxIntensity);
        };
        close.AddTransition(close2Open);

        lightFSM = new FSMMachine("LightFSM",open);
        lightFSM.AddState(close);
    }

    private bool FadeTo(float f)
    {
        if(Mathf.Abs(myLight.intensity - f)<= 0.05f)
        {
            myLight.intensity = f;
            return true;
        }
        else
        {
            int flag = myLight.intensity>f?-1:1;
            myLight.intensity+=Time.deltaTime *fadeSpeed*flag;
            return false;
        }

    }
}

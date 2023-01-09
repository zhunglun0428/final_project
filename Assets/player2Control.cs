using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
public class player2Control : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    [SerializeField]
    private float moveSpeed = 500f;
    public float secToWait = 0.05f;
    public Text DiceSide;
    public Vector3 offset;
    public whoTurn whoTurn_;
    public GameObject player;
    public rollTheDice _rollTheDice;
    public int waypoint=1;
    int waypoint_=1;
    bool moving=false;
    //public Transform[] waypoints;
    public Sprite[] diceSides;
    //private SpriteRenderer rend;
    public GameObject rend;
    public GameObject _road;
    public GameObject _billboard;
    public Transform[] road;
    /////////////////處理玩家位置功能////////////////////
    public bool buyOrPay = false;
    public bool chance = false;
    public bool money = false;
    public bool fate = false;
    //////////////////////////////////////////////////////
    public Transform[] property;
    public GameObject _property;
    public Transform[] Road;
    public Transform[] billboard;
    public Color[] color_list;
    int color_index; 
    public TextMeshProUGUI P1_moneyTxt;
    public int P1_money=15000;
    public TMP_Text P1_landTxt;
    public int P1_land = 0;
    public int value;
    public GameObject question,question2;
    public GameObject pareantQuestion;
    private string[] yesQ;
    private string[] noQ;
    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        //diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.GetComponent<Image>().sprite = diceSides[5];
        P1_moneyTxt.text=""+P1_money;
        road = _road. GetComponentsInChildren<Transform>(true);
        property = _property.GetComponentsInChildren<Transform>(true);
        //Road =  _road. GetComponentsInChildren<GameObject>(true);
        billboard = _billboard.GetComponentsInChildren<Transform>(true);
        var path = @"D:\Question\yes.txt";
        yesQ = File.ReadAllLines(path);
        var path2 = @"D:\Question\no.txt";
        noQ = File.ReadAllLines(path2);
    }

    // Update is called once per frame
    void Update()
    {
        if(moving){
            if(waypoint_%62==waypoint){
                moving=false;
                Time.timeScale= 0f;
                if(buyOrPay){
                    StartCoroutine("BuyOrPay");
                 
                    buyOrPay=false;
                }
                else if(money){
                    StartCoroutine("touchMoneyLand");
                  
                    money = false;
                }
                else if(fate){
                    StartCoroutine("touchFateLand");
                    
                    fate = false;
                }
                
                Time.timeScale=1f;

            }
            //player.transform.position = ground[waypoint].transform.position+offset;
            player.transform.position = Vector3.MoveTowards(player.transform.position,
            road[waypoint_].transform.position+offset,
            moveSpeed * Time.deltaTime);
            if(player.transform.position==road[waypoint_].transform.position+offset){
                waypoint_=(waypoint_+1)%62;
            }
            
        } 
    }
    public void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }
    public IEnumerator RollTheDice(){
        //int randomDiceSide = 0;
        if(whoTurn_.getWhoTurn()==-1){
            int randomDiceSide = 0;
        
            for (int i = 0; i <= 15; i++)
            {
                randomDiceSide = UnityEngine.Random.Range(0, 6);
                
                rend.GetComponent<Image>().sprite = diceSides[randomDiceSide];
                //WaitForSecondsDo(secToWait);
                yield return new WaitForSeconds(0.1f);
            }
            
            waypoint_=waypoint;
            //waypoint=(waypoint+randomDiceSide+1)%62;
            waypoint=(waypoint+3+1)%62;
            moving=true;
            /////////////處理玩家位置功能//////////////////
            string tag_ = road[waypoint-1].tag;
            if(tag_=="money"){
                money = true;
            }
            else if(tag_=="chance"){
                chance=true;
            }
            else if(tag_=="fate"){
                fate = true;
            }
            else{
                buyOrPay=true;
            }
        }
          
        
        //////////////////////////////////////////
    }
    public IEnumerator turnPlayer(){
        //int value;
        
        whoTurn_.turnPlayer();
        yield return null;
    }
    public IEnumerator BuyOrPay(){
        string tag_ = road[waypoint-1].tag; 
        bool canPayForProperty = true; //避免重複扣錢
        //yield return new WaitForSeconds(10f);
        int turnOrnot=0;
        for(int i=0;i<property.Length;i++){
            if(property[i].tag==tag_){
                if(property[i].GetComponent<MeshRenderer>().material.color==color_list[0]){
                    RespondDialogUI.Instance.ShowRespond("這是別人的土地 損失1000元", () => {
                        
                        P1_money-=1000;
                        P1_moneyTxt.text = "" + P1_money;
                        _rollTheDice.SetMoney();
                    }, () => {
                    });
                    turnOrnot++;
                    break;
                    
                }else{
                    //int random1 = UnityEngine.Random.Range(0, 1);
                    int random1 = 0;
                    if(random1==0){
                        int random2 = UnityEngine.Random.Range(0, yesQ.Length);
                        QuestionDialogUI2.Instance.ShowQuestion(yesQ[random2], () => {
                            QuestionDialogUI.Instance.ShowQuestion("你想要購買"+tag_+"嗎?", () => {
                        //P1_money = Convert.ToInt32(P1_moneyTxt.text);
                                if(canPayForProperty){
                                    property[i].GetComponent<MeshRenderer>().material.color = color_list[1];
                                    P1_money-=2000;
                                    P1_moneyTxt.text=""+P1_money;
                                    P1_land ++;
                                    P1_landTxt.text = ""+P1_land;
                                    buyOrPay=false;
                                    canPayForProperty=false;
                                    for(int j=0;j<billboard.Length;j++){
                                        if(billboard[j].tag==tag_){
                                            billboard[j].gameObject.SetActive(false);
                                        }
                                    }
                                    RespondDialogUI.Instance.ShowRespond("購買成功", () => {
                                        
                                    }, () => {
                                    });
                                }
                                }, () => {
                                    RespondDialogUI.Instance.ShowRespond("購買失敗", () => {
                                        
                                    }, () => {
                                    });
                                    buyOrPay=false;
                                    
                                });
                            }, () => {
                                RespondDialogUI.Instance.ShowRespond("回答錯誤無法購買", () => {
                                        
                                }, () => {
                                });
                                value= 0;
                                //Debug.Log(value);  
                                
                            });
                        Debug.Log("test");
                        turnOrnot++;
                        break;
                    }else{
                        int random2 = UnityEngine.Random.Range(0, noQ.Length);
                        QuestionDialogUI2.Instance.ShowQuestion(noQ[random2], () => {
                                RespondDialogUI.Instance.ShowRespond("回答錯誤無法購買", () => {
                                        
                                }, () => {
                                });
                                value= 0;
                                //Debug.Log(value); 
                            }, () => {
                                 
                                QuestionDialogUI.Instance.ShowQuestion("你想要購買"+tag_+"嗎?", () => {
                        //P1_money = Convert.ToInt32(P1_moneyTxt.text);
                                if(canPayForProperty){
                                    property[i].GetComponent<MeshRenderer>().material.color = color_list[1];
                                    P1_money-=2000;
                                    P1_moneyTxt.text=""+P1_money;
                                    P1_land ++;
                                    P1_landTxt.text = ""+P1_land;
                                    buyOrPay=false;
                                    canPayForProperty=false;
                                    for(int j=0;j<billboard.Length;j++){
                                        if(billboard[j].tag==tag_){
                                            billboard[j].gameObject.SetActive(false);
                                        }
                                    }
                                    RespondDialogUI.Instance.ShowRespond("購買成功", () => {
                                        
                                    }, () => {
                                    });
                                }
                                }, () => {
                                    RespondDialogUI.Instance.ShowRespond("購買失敗", () => {
                                        
                                    }, () => {
                                    });
                                    buyOrPay=false;
                                    
                                });
                            });
                        Debug.Log("test");
                        turnOrnot++;
                        break;
                    }
                    
                }
                
                
            } 
        }
        if(turnOrnot==0){
            yield return new WaitForSeconds(2f);
            whoTurn_.turnPlayer();
        }
        
        buyOrPay=false;

        yield return null;
    }
    
    public IEnumerator touchMoneyLand(){
        bool canPayForMoney = true;
        RespondDialogUI.Instance.ShowRespond("You got $200 ,Congrats!!", () => {
            if(canPayForMoney){
                P1_money = Convert.ToInt32(P1_moneyTxt.text);
                P1_money+=200;
                money=false;
                P1_moneyTxt.text=""+P1_money;
                canPayForMoney = false;
            }
         
        }, () => {
            // Do nothing on No
            money=false;
            
        });
        
        yield return null;
    }     
    public IEnumerator touchFateLand(){
        int randomFate = UnityEngine.Random.Range(0, 5);
        bool canPayForFate = true;
        if(randomFate==0){
            RespondDialogUI.Instance.ShowRespond("今天是你的生日,獲得500元", () => {
                if(canPayForFate){
                    P1_money += 500;
                    P1_moneyTxt.text = "" + P1_money;
                    canPayForFate = false;
                }
                
            }, () => {
                
            });
            
            yield return null;
        }
        else if(randomFate==1){
            RespondDialogUI.Instance.ShowRespond("系學會收取系費,損失2000元", () => {
                if(canPayForFate){
                    P1_money =P1_money - 2000;
                    P1_moneyTxt.text = "" + P1_money;
                    canPayForFate = false;
                }
            }, () => {
            });
         
            yield return null;
        }
        else if(randomFate==2){
            RespondDialogUI.Instance.ShowRespond("籃球系際冠軍,獲得1000元", () => {
                if(canPayForFate){
                    P1_money += 1000;
                    P1_moneyTxt.text = "" + P1_money;
                    canPayForFate = false;
                }
               
            }, () => {
               
            });
            
            yield return null;
        }
        else if(randomFate==3){
            RespondDialogUI.Instance.ShowRespond("小偷光顧宿舍,損失1000元", () => {
                if(canPayForFate){
                    P1_money += -1000;
                    P1_moneyTxt.text = "" + P1_money;
                    canPayForFate = false;
                }
              
            }, () => {
                
            });
            
            yield return null;
        }
        
        else if(randomFate==4){
            RespondDialogUI.Instance.ShowRespond("夜唱偷開酒被服務生抓到,損失500元", () => {
                if(canPayForFate){
                    P1_money += -500;
                    P1_moneyTxt.text = "" + P1_money; 
                    canPayForFate = false;
                }
                
            }, () => {
                
            });
            
            yield return null;
        }
        fate=false;
        
        yield return null;
    }
    public void SetMoney(){
        P1_money+=1000;
        P1_moneyTxt.text = "" + P1_money;
    }
}

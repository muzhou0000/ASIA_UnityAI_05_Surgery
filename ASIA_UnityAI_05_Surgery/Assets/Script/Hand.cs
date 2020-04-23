using UnityEngine;
using UnityEngine.UI;
using MLAgents;
using MLAgents.Sensors;

public class Hand : Agent
{
    private Rigidbody robot_rig, Kidney_rig,Lung_rig,Liver_rig,heart_rig, stomach_rig;
    [Header("速度"), Range(1, 50)]
    public float speed;
    private Animator ani;


    private void Start()
    {
        ani = GetComponent<Animator>();
        robot_rig = GetComponent<Rigidbody>();
        Kidney_rig = GameObject.Find("kidney").GetComponent<Rigidbody>();
        Lung_rig = GameObject.Find("lung").GetComponent<Rigidbody>();
        Liver_rig = GameObject.Find("liver").GetComponent<Rigidbody>();
        heart_rig = GameObject.Find("heart").GetComponent<Rigidbody>();
        stomach_rig = GameObject.Find("stomach").GetComponent<Rigidbody>();
    }
    /// <summary>
    /// 每次開始時重新設定人跟球的位置
    /// </summary>
    public override void OnEpisodeBegin()
    {
        //把加速度跟角度加速度歸零
        robot_rig.velocity = Vector3.zero;
        robot_rig.angularVelocity = Vector3.zero;
        Kidney_rig.velocity = Vector3.zero;
        Kidney_rig.angularVelocity = Vector3.zero;
        //隨機出現人的位置
        Vector3 posRobot = new Vector3(Random.Range(-5f, 0.5f), 1.6f, 9.21f);
        transform.position = posRobot;
        //隨機出現球的位置
        Vector3 posKidney = new Vector3(Random.Range(-5f, 0.5f), 1.6f, Random.Range(7.57f, 10.85f));
        Kidney_rig.position = posKidney;
        Vector3 posLung = new Vector3(Random.Range(-5f, 0.5f), 1.6f, Random.Range(7.57f, 10.85f));
        Lung_rig.position = posLung;
        Vector3 posLiver = new Vector3(Random.Range(-5f, -2.6f), 1.6f, 10.05f);
        Liver_rig.position = posLiver;
        Vector3 posheart = new Vector3(-2.58f, 1.6f, 11.06f);
        heart_rig.position = posheart;
        Vector3 posstomach = new Vector3(0.46477f, 1.6f, 11.45f);
        stomach_rig.position = posstomach;

        Kidney.complate = false;
        Other.Oth_complate = false;
    }
    /// <summary>
    /// 收集資料
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        //加入觀測資料
        sensor.AddObservation(transform.position);
        sensor.AddObservation(Kidney_rig.position);
        sensor.AddObservation(robot_rig.velocity.x);
        sensor.AddObservation(robot_rig.velocity.z);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        robot_rig.AddForce(control * speed);


        if (Kidney.complate)
        {
            SetReward(1);
            EndEpisode();
        }
        if (transform.position.y < 0 || Other.Oth_complate)
        {
            SetReward(-1);
            EndEpisode();
        }
    }
    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}

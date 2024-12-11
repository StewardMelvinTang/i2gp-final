using UnityEngine;
using UnityEngine.UI;

public class CalibrationMechanism : MonoBehaviour
{
    public RawImage progressBar;         // 进度条
    public RectTransform pointer;     // 动态指针
    public RectTransform judgeZone;   // 判定窗口

    public float moveSpeed = 0.01f;      // 指针移动速度
    private bool isMovingRight = true; // 指针移动方向

    private bool isCalibrating = false; // 校准进行中
    private int successCount = 0;      // 成功次数计数
    private int failCount = 0;         // 失败次数计数
    public int requiredSuccess = 3;   // 需要成功次数
    public int maxFails = 1;          // 最大失败次数

    private float judgeZoneMinX = -72.6f;  // judgeZone 最小 x
    private float judgeZoneMaxX = 82.6f;   // judgeZone 最大 x
    private float pointerMinX = -92.6f;   // pointer 最小 x
    private float pointerMaxX = 102.6f;   // pointer 最大 x

    void Start()
    {
        Application.targetFrameRate = 60;  
        StartCalibration();  // 游戏开始时调用校准方法
    }

    void Update()
    {
        if (isCalibrating)
        {
            MovePointer(); // 动态指针移动逻辑
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckCalibration(); // 检查校准结果
            }
        }
    }

    // void MovePointer()
    // {
    //     float step = moveSpeed * Time.deltaTime;
    //     Vector3 currentPosition = pointer.localPosition;

    //     // 控制指针在指定范围内移动
    //     if (isMovingRight)
    //     {
    //         pointer.localPosition += Vector3.right * step;
    //         if (pointer.localPosition.x >= pointerMaxX)
    //         {
    //             isMovingRight = false; // 到达右边界，反转方向
    //         }
    //     }
    //     else
    //     {
    //         pointer.localPosition += Vector3.left * step;
    //         if (pointer.localPosition.x <= pointerMinX)
    //         {
    //             isMovingRight = true; // 到达左边界，反转方向
    //         }
    //     }
    // }

    void MovePointer()
{
    float step = moveSpeed;  // 移除 Time.deltaTime 影响，直接控制移动步长
    Vector3 currentPosition = pointer.localPosition;

    // 控制指针在指定范围内移动
    if (isMovingRight)
    {
        pointer.localPosition += Vector3.right * step;
        if (pointer.localPosition.x >= pointerMaxX)
        {
            isMovingRight = false; // 到达右边界，反转方向
        }
    }
    else
    {
        pointer.localPosition += Vector3.left * step;
        if (pointer.localPosition.x <= pointerMinX)
        {
            isMovingRight = true; // 到达左边界，反转方向
        }
    }
}

// void MovePointer()
// {
//     float step = moveSpeed * Time.deltaTime;  // 使用 Time.deltaTime 来确保平滑过渡
//     Vector3 currentPosition = pointer.localPosition;

//     // 控制指针在指定范围内移动
//     if (isMovingRight)
//     {
//         pointer.localPosition += Vector3.right * step;
//         if (pointer.localPosition.x >= pointerMaxX)
//         {
//             isMovingRight = false; // 到达右边界，反转方向
//         }
//     }
//     else
//     {
//         pointer.localPosition += Vector3.left * step;
//         if (pointer.localPosition.x <= pointerMinX)
//         {
//             isMovingRight = true; // 到达左边界，反转方向
//         }
//     }
// }



    void CheckCalibration()
    {
        // 判定条件：指针是否在 judgeZone 位置 ±20 之间
        float judgeZoneX = judgeZone.localPosition.x;
        float pointerX = pointer.localPosition.x;

        if (pointerX >= judgeZoneX - 20 && pointerX <= judgeZoneX + 20)
        {
            successCount++;
            Debug.Log($"Success! Count: {successCount}");
            
            if (successCount >= requiredSuccess)
            {
                Debug.Log("Calibration Complete!");
                isCalibrating = false; // 停止校准
                return;
            }

            AdjustJudgeZone(); // 成功后调整判定窗口
        }
        else
        {
            failCount++;
            Debug.Log($"Failed! Fail Count: {failCount}");

            if (failCount >= maxFails)
            {
                Debug.Log("Max Fails Reached! Restarting Calibration...");
                RestartCalibration(); // 重置校准
                return;
            }
        }
    }

    void AdjustJudgeZone()
    {
        // 随机调整判定窗口的位置
        float newXPosition = Random.Range(judgeZoneMinX, judgeZoneMaxX);
        judgeZone.localPosition = new Vector3(newXPosition, judgeZone.localPosition.y, judgeZone.localPosition.z);

        // // 随机调整判定窗口的大小
        // float newWidth = Random.Range(50, 150); // 根据难度调整范围
        // judgeZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }

    public void StartCalibration()
    {
        isCalibrating = true;
        successCount = 0; // 初始化成功计数
        failCount = 0;    // 初始化失败计数
        AdjustJudgeZone(); // 初始化判定窗口
    }

    void RestartCalibration()
    {
        successCount = 0;
        failCount = 0;
        Debug.Log("Calibration Restarted!");
        AdjustJudgeZone(); // 初始化判定窗口
    }
}

using UnityEngine;
using UnityEngine.UI;
using static Camp;
using System.Collections.Generic;



public class TargetingReticle : MonoBehaviour
{
    public GameObject Aimpoint;
    public float width = 100f; // 矩形宽度
    public float height = 50f; // 矩形高度
    public float lineThickness = 2f;
    private GameObject reticle;
    public float zOffset = 0f;
    public float radius = 10f; // 圆的半径
    public int segments = 100; // 圆的细分数，数值越高越平滑
    public float lineWidth = 1.5f; // 边框的宽度

    private LineRenderer[] lineRenderers;
    public LayerMask layerMask;
    public List<GameObject> newLockableEnemies = new List<GameObject>();
    public GameObject CurTarget;
    private GameObject previousTarget;
    private List<GameObject> enemies;
    private GameObject reticleCircle;
    private GameObject Aimline;

    public delegate void targetingReticle();
    public event targetingReticle LockedOn;



    void Awake()
    {
        LockedOn += ReticalAlert;
        LockedOn += Aim;

    }
    void Start()
    {
        DrawReticle();
        List<GameObject> enemies = CampManager.instance.GetCampObject(CampType.Enemy);
        CreateCircularReticle();
    }

    void Update()
    {
        //Rect screenBounds = GetReticleScreenBounds();
        //Debug.Log("Screen Bounds: " + screenBounds);
        DetectEnemyObject();
        LockOn();
        if (LockOn())
        {
            if (CurTarget != previousTarget)
            {
                Debug.Log(CurTarget);
                previousTarget = CurTarget;
            }
            LockedOn?.Invoke();
        }
        else
        {
            if (previousTarget != null)
            {
                Debug.Log("No Lock On");
                previousTarget = null;
            }
            LockedOn?.Invoke();
        }

    }

    void DrawReticle()
    {
        reticle = new GameObject("TargetReticle");
        reticle.transform.SetParent(this.transform);
        reticle.transform.localPosition = Vector3.zero;

        lineRenderers = new LineRenderer[4];

        // 初始化每条边的 LineRenderer
        lineRenderers[0] = CreateLine(new Vector3(-width / 2, height / 2, zOffset), new Vector3(width / 2, height / 2, zOffset)); // 上边
        lineRenderers[1] = CreateLine(new Vector3(-width / 2, -height / 2, zOffset), new Vector3(width / 2, -height / 2, zOffset)); // 下边
        lineRenderers[2] = CreateLine(new Vector3(-width / 2, -height / 2, zOffset), new Vector3(-width / 2, height / 2, zOffset)); // 左边
        lineRenderers[3] = CreateLine(new Vector3(width / 2, -height / 2, zOffset), new Vector3(width / 2, height / 2, zOffset)); // 右边
    }

    // 创建单条边的 LineRenderer
    LineRenderer CreateLine(Vector3 start, Vector3 end)
    {
        Aimline = new GameObject("Line");
        Aimline.transform.SetParent(reticle.transform);
        Aimline.layer = LayerMask.NameToLayer("UI");

        LineRenderer lineRenderer = Aimline.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 设置材质
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false; // 保持相对父对象的局部坐标

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        return lineRenderer;
    }

    // 动态更新瞄准框的大小
    void UpdateReticleSize(float newWidth, float newHeight)
    {
        width = newWidth;
        height = newHeight;

        LineRenderer lineRenderer = Aimline.GetComponent<LineRenderer>();
        // 更新四条边的位置
        lineRenderers[0].SetPosition(0, new Vector3(-width / 2, height / 2, zOffset)); // 上边
        lineRenderers[0].SetPosition(1, new Vector3(width / 2, height / 2, zOffset));

        lineRenderers[1].SetPosition(0, new Vector3(-width / 2, -height / 2, zOffset)); // 下边
        lineRenderers[1].SetPosition(1, new Vector3(width / 2, -height / 2, zOffset));

        lineRenderers[2].SetPosition(0, new Vector3(-width / 2, -height / 2, zOffset)); // 左边
        lineRenderers[2].SetPosition(1, new Vector3(-width / 2, height / 2, zOffset));

        lineRenderers[3].SetPosition(0, new Vector3(width / 2, -height / 2, zOffset)); // 右边
        lineRenderers[3].SetPosition(1, new Vector3(width / 2, height / 2, zOffset));
    }

    // 获取屏幕空间的瞄准框范围
    Rect GetReticleScreenBounds()
    {
        Vector3 minScreenPoint = new Vector3(float.MaxValue, float.MaxValue, 0);
        Vector3 maxScreenPoint = new Vector3(float.MinValue, float.MinValue, 0);

        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                Vector3 worldPoint = lineRenderer.transform.TransformPoint(lineRenderer.GetPosition(i));
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);
                minScreenPoint = Vector3.Min(minScreenPoint, screenPoint);
                maxScreenPoint = Vector3.Max(maxScreenPoint, screenPoint);
            }
        }

        return Rect.MinMaxRect(minScreenPoint.x, minScreenPoint.y, maxScreenPoint.x, maxScreenPoint.y);
    }

    private void DetectEnemyObject()
    {
        if (CampManager.instance.CampCountChange(CampType.Enemy))
        {
            enemies = CampManager.instance.GetCampObject(CampType.Enemy);
        }

    }
    bool LockOn()
    {
        Rect reticleBounds = GetReticleScreenBounds();
        Vector2 reticleCenter = reticleBounds.center;

        // 更新新锁定的敌人列表
        newLockableEnemies.Clear();
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; // 检查敌人是否为空
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(enemy.transform.position);

            // 仅添加在瞄准框内的敌人
            // 将世界坐标转为视口坐标（0-1范围，表示是否在视野内）
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(enemy.transform.position);

            // 检查敌人是否在摄像机视野内（视口坐标的x和y都在0-1范围内），且在瞄准框内
            if (reticleBounds.Contains(screenPosition) && viewportPosition.z > 0 && viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                newLockableEnemies.Add(enemy);
            }
        }

        // 如果没有敌人被锁定，则重置当前目标并返回 false
        if (newLockableEnemies.Count == 0)
        {
            CurTarget = null;
            return false;
        }

        // 查找距离瞄准框中心最近的敌人
        float minDistance = float.MaxValue;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in newLockableEnemies)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
            float distance = Vector2.Distance(reticleCenter, screenPos);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        // 设置当前目标并返回 true
        CurTarget = closestEnemy;
        return true;
    }

    private void ReticalAlert()
    {
        if (LockOn())
        {

            foreach (LineRenderer rt in lineRenderers)
            {
                LineRenderer image = rt.gameObject.GetComponent<LineRenderer>();
                image.startColor = Color.red;
                image.endColor = Color.red;


            }


        }
        else
        {
            foreach (LineRenderer rt in lineRenderers)
            {
                LineRenderer image = rt.gameObject.GetComponent<LineRenderer>();
                image.startColor = Color.green;
                image.endColor = Color.green;


            }
        }



    }

    void CreateCircularReticle()
    {


        // 创建空的 GameObject 作为圆形边框
        reticleCircle = new GameObject("CircularReticle");
        reticleCircle.layer = LayerMask.NameToLayer("UI");
        reticleCircle.transform.SetParent(reticle.transform);
        reticleCircle.transform.localPosition = new Vector3(0, 0, zOffset);

        // 添加 LineRenderer 组件
        LineRenderer lineRenderer = reticleCircle.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.loop = true; // 设置为循环，使首尾相连形成圆形
        lineRenderer.positionCount = segments;

        // 设置 LineRenderer 的材质和颜色
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // 计算圆的每个点的位置
        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * 2 * Mathf.PI;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }

        reticleCircle.SetActive(false);


    }

    void Aim()
    {
        if (CurTarget != null)
        {
            reticleCircle.SetActive(true);
            reticleCircle.transform.position =CurTarget.transform.position;
        }
        else
        {
            reticleCircle.SetActive(false);
            reticleCircle.transform.position = Vector3.zero;
        }

    }
}



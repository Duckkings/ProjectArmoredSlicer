using UnityEngine;
using UnityEngine.UI;
using static Camp;
using System.Collections.Generic;



public class TargetingReticle : MonoBehaviour
{
    public GameObject Aimpoint;
    public float width = 100f; // ���ο��
    public float height = 50f; // ���θ߶�
    public float lineThickness = 2f;
    private GameObject reticle;
    public float zOffset = 0f;
    public float radius = 10f; // Բ�İ뾶
    public int segments = 100; // Բ��ϸ��������ֵԽ��Խƽ��
    public float lineWidth = 1.5f; // �߿�Ŀ��

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

        // ��ʼ��ÿ���ߵ� LineRenderer
        lineRenderers[0] = CreateLine(new Vector3(-width / 2, height / 2, zOffset), new Vector3(width / 2, height / 2, zOffset)); // �ϱ�
        lineRenderers[1] = CreateLine(new Vector3(-width / 2, -height / 2, zOffset), new Vector3(width / 2, -height / 2, zOffset)); // �±�
        lineRenderers[2] = CreateLine(new Vector3(-width / 2, -height / 2, zOffset), new Vector3(-width / 2, height / 2, zOffset)); // ���
        lineRenderers[3] = CreateLine(new Vector3(width / 2, -height / 2, zOffset), new Vector3(width / 2, height / 2, zOffset)); // �ұ�
    }

    // ���������ߵ� LineRenderer
    LineRenderer CreateLine(Vector3 start, Vector3 end)
    {
        Aimline = new GameObject("Line");
        Aimline.transform.SetParent(reticle.transform);
        Aimline.layer = LayerMask.NameToLayer("UI");

        LineRenderer lineRenderer = Aimline.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // ���ò���
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false; // ������Ը�����ľֲ�����

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        return lineRenderer;
    }

    // ��̬������׼��Ĵ�С
    void UpdateReticleSize(float newWidth, float newHeight)
    {
        width = newWidth;
        height = newHeight;

        LineRenderer lineRenderer = Aimline.GetComponent<LineRenderer>();
        // ���������ߵ�λ��
        lineRenderers[0].SetPosition(0, new Vector3(-width / 2, height / 2, zOffset)); // �ϱ�
        lineRenderers[0].SetPosition(1, new Vector3(width / 2, height / 2, zOffset));

        lineRenderers[1].SetPosition(0, new Vector3(-width / 2, -height / 2, zOffset)); // �±�
        lineRenderers[1].SetPosition(1, new Vector3(width / 2, -height / 2, zOffset));

        lineRenderers[2].SetPosition(0, new Vector3(-width / 2, -height / 2, zOffset)); // ���
        lineRenderers[2].SetPosition(1, new Vector3(-width / 2, height / 2, zOffset));

        lineRenderers[3].SetPosition(0, new Vector3(width / 2, -height / 2, zOffset)); // �ұ�
        lineRenderers[3].SetPosition(1, new Vector3(width / 2, height / 2, zOffset));
    }

    // ��ȡ��Ļ�ռ����׼��Χ
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

        // �����������ĵ����б�
        newLockableEnemies.Clear();
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; // �������Ƿ�Ϊ��
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(enemy.transform.position);

            // ���������׼���ڵĵ���
            // ����������תΪ�ӿ����꣨0-1��Χ����ʾ�Ƿ�����Ұ�ڣ�
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(enemy.transform.position);

            // �������Ƿ����������Ұ�ڣ��ӿ������x��y����0-1��Χ�ڣ���������׼����
            if (reticleBounds.Contains(screenPosition) && viewportPosition.z > 0 && viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                newLockableEnemies.Add(enemy);
            }
        }

        // ���û�е��˱������������õ�ǰĿ�겢���� false
        if (newLockableEnemies.Count == 0)
        {
            CurTarget = null;
            return false;
        }

        // ���Ҿ�����׼����������ĵ���
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

        // ���õ�ǰĿ�겢���� true
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


        // �����յ� GameObject ��ΪԲ�α߿�
        reticleCircle = new GameObject("CircularReticle");
        reticleCircle.layer = LayerMask.NameToLayer("UI");
        reticleCircle.transform.SetParent(reticle.transform);
        reticleCircle.transform.localPosition = new Vector3(0, 0, zOffset);

        // ��� LineRenderer ���
        LineRenderer lineRenderer = reticleCircle.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.loop = true; // ����Ϊѭ����ʹ��β�����γ�Բ��
        lineRenderer.positionCount = segments;

        // ���� LineRenderer �Ĳ��ʺ���ɫ
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // ����Բ��ÿ�����λ��
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



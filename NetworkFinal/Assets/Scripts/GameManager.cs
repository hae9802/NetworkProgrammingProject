using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private class InitTarget
    {
        private float scaleX;
        private float scaleY;
        private float posY;

        public float SX() { return scaleX; }
        public float SY() { return scaleY; }
        public float PY() { return posY; }

        public InitTarget(Transform tf)
        {
            scaleX = tf.localScale.x;
            scaleY = tf.localScale.y;
            posY = tf.position.y;
        }
    }

    // �ٰ����� �ӵ� ����
    public float speed1;
    public float speed2;

    // �ٴ� ��ũ�Ѹ� �ӵ� ����
    public float groundSpeed;

    // �ٴ��� �ݺ��� ���� ����
    private Renderer groundRenderer;

    // Ÿ���� ũ��� �ʱ� ��ġ�� ���� ����
    private GameObject target;
    private InitTarget initTarget;

    private void Start()
    {
        target = GameObject.Find("Target");
        initTarget = new InitTarget(target.transform);
        groundRenderer = GameObject.Find("Ground").GetComponent<Renderer>();
    }

    private void Update()
    {
        float offset = Time.time * groundSpeed;

        Vector2 textureOffset = new Vector2(offset, 0);
        groundRenderer.material.mainTextureOffset = textureOffset;
        if (Input.GetKey(KeyCode.Space))
        {
            TargetCloser();
            GroundScrolling();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Hit R");
            ResetTarget();
        }
    }

    private void GroundScrolling()
    {
        Debug.Log("Ground Scroll Call");
        Debug.Log(groundRenderer.name);
        float offset = Time.time * groundSpeed;

        Vector2 textureOffset = new Vector2(offset, 0);
        groundRenderer.material.mainTextureOffset = textureOffset;
        Debug.Log(offset);
    }

    /// <summary>
    /// Ÿ���� �Ÿ��� ������ �մϴ�.
    /// </summary>
    private void TargetCloser()
    {
        Vector2 targetScale = target.transform.localScale;
        float targetYPos = target.transform.position.y;
        targetScale.x += speed1 * Time.deltaTime;
        targetScale.y += speed1 * Time.deltaTime;
        if (targetYPos > 0)
            targetYPos -= speed2 * Time.deltaTime;
        target.transform.localScale = targetScale;
        target.transform.position = new Vector2(0, targetYPos);
    }

    /// <summary>
    /// Ÿ���� ��ġ�� �ʱ� ��ġ�� �ǵ����ϴ�.
    /// </summary>
    private void ResetTarget()
    {
        target.transform.position = new Vector2(0, initTarget.PY());
        target.transform.localScale = new Vector2(initTarget.SX(), initTarget.SY());
    }
 }

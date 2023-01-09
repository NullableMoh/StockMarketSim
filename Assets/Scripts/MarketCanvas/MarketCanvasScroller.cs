using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RvveSplit.MarketCanvas
{
    public class MarketCanvasScroller : MonoBehaviour
    {
        [SerializeField] float scrollSpeed;
        [SerializeField] RectTransform canvasToScroll, topLimit, bottomLimit;

        float scrollInput;

        PlayerInputActions inputActions;

        private void Awake()
        {
            inputActions = new();
            inputActions.Player.Enable();
        }

        private void OnEnable()
        {
            inputActions.Player.Scroll.performed += OnScrollPerformed;
            inputActions.Player.Scroll.canceled += EndScroll;
        }

        private void OnDisable()
        {
            inputActions.Player.Scroll.performed -= OnScrollPerformed;
            inputActions.Player.Scroll.canceled -= EndScroll;
        }

        void OnScrollPerformed(InputAction.CallbackContext ctx)
        {
            scrollInput = -ctx.ReadValue<float>();
        }

        void EndScroll(InputAction.CallbackContext ctx)
        {
            scrollInput = 0f;
        }

        private void Update()
        {
            if (!canvasToScroll.gameObject.activeInHierarchy) return;

            if (canvasToScroll.position.y < topLimit.position.y && canvasToScroll.position.y > bottomLimit.position.y)
                canvasToScroll.transform.position = Vector3.up * scrollSpeed * scrollInput * Time.deltaTime 
                    + Vector3.up * Mathf.Clamp(canvasToScroll.transform.position.y, bottomLimit.position.y + 0.1f, topLimit.position.y - 0.1f);

            canvasToScroll.transform.position = Vector3.up * Mathf.Clamp(canvasToScroll.transform.position.y, bottomLimit.position.y + 0.1f, topLimit.position.y - 0.1f);
        }
    }
}

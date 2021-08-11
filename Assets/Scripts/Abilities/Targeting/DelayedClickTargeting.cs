using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Abilities{
    [CreateAssetMenu(fileName ="Delayed Click Targeting", menuName = "Abilities/Targeting/Delayed Click", order = 0)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        [SerializeField] Texture2D cursorTexture;
        [SerializeField] Vector2 cursorHotspot;
        [SerializeField] LayerMask layerMask;
        [SerializeField] float areaEffectRadius;
        [SerializeField] Transform targetingPrefab;

        Transform targetingPrefabInstance = null;

        public override void StartTargeting(AbilityData data, Action finished)
        {
            PlayerController playerController = data.GetUser().GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(data, playerController, finished));
        }

        IEnumerator Targeting(AbilityData data, PlayerController playerController, Action finished){
            playerController.enabled = false;

            if (targetingPrefabInstance == null) {
                targetingPrefabInstance = Instantiate(targetingPrefab);
            } else {
                targetingPrefabInstance.gameObject.SetActive(true);
            }

            targetingPrefabInstance.localScale = new Vector3(areaEffectRadius * 2, 1, areaEffectRadius * 2);

            while (true) {
                Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

                RaycastHit raycastHit;

                if (Physics.Raycast(PlayerController.GetMouseRay(), out raycastHit, 1000, layerMask)){

                    targetingPrefabInstance.position = raycastHit.point;

                    if (Input.GetMouseButtonDown(0)) {

                        yield return new WaitWhile(() => Input.GetMouseButton(0));

                        playerController.enabled = true;
                        targetingPrefabInstance.gameObject.SetActive(false);

                        data.SetTargetedPoint(raycastHit.point);
                        data.SetTargets(GetGameObjectsInRadius(raycastHit.point));

                        finished();
                        yield break;
                    }
                }

                yield return null;
            }
        }

        private IEnumerable<GameObject> GetGameObjectsInRadius(Vector3 point)
        {
            RaycastHit[] hits = Physics.SphereCastAll(point, areaEffectRadius, Vector3.up, 0);

            foreach (RaycastHit hit in hits){
                yield return hit.collider.gameObject;
            }
        }
    }
}
using PathCreation;
using TMPro;
using UnityEngine;

namespace PathCreation.Examples {

    [ExecuteInEditMode]
    public class PathPlacer : PathSceneTool {

        public GameObject prefab;
        public GameObject holder;
        public float spacing = 3;
        public bool isPark;
        const float minSpacing = .1f;
        float dst = 8;
        void Generate () {
            if (pathCreator != null && prefab != null && holder != null) {
                DestroyObjects ();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                if (isPark)
                {
                     dst = 40;
                    int adet = 0;
                    while (adet < 5)
                    {
                        Vector3 point = path.GetPointAtDistance(dst);
                        Quaternion rot = path.GetRotationAtDistance(dst);
                        GameObject park = Instantiate(prefab, point+new Vector3(-3,0,0),Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y-180, rot.eulerAngles.z+90), holder.transform);
                        park.transform.GetChild(3).GetComponent<TextMeshPro>().text = (park.transform.GetSiblingIndex()+1).ToString();
                        dst += spacing;
                        adet++;
                    }
                }
                else
                {
                     dst = 8;
                    int adet = 0;
                    while (adet < 5)
                    {
                        Vector3 point = path.GetPointAtDistance(dst);
                        Quaternion rot = path.GetRotationAtDistance(dst);
                        Instantiate(prefab, point, rot, holder.transform);
                        dst += spacing;
                        adet++;
                    }
                }
               
            }
        }

        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}
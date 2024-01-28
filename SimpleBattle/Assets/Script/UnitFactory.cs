using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFactory : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    public Unit createUnit(Transform parent, string resName) {
        try
        {
            GameObject prefab = Resources.Load<GameObject>(resName);
            GameObject obj = Instantiate(prefab, parent);
            obj.AddComponent<Unit>();
            Unit unit = obj.GetComponent<Unit>();
            unit.initSet();
            return unit;
        }
        catch (System.Exception)
        {

        }
        return null;
    }
}

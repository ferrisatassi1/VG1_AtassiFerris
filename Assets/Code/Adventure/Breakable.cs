using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class Breakable : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Break()
        {
            Destroy(gameObject);
        }
    }
}


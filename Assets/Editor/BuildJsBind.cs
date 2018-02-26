using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;
namespace JSInterface
{
    public class BuildJsBind : MonoBehaviour
    {



        public static void BuildWrapper()
        {
            //Dictionary<System.Type, Dictionary<System.Type, System.Type>> TypeTree = new Dictionary<System.Type, Dictionary<System.Type, System.Type>>();

            List<System.Type> sortedTypes = new List<System.Type>(CustomSettings.customTypeList);
            sortedTypes.Sort((a, b) =>
            {
                var aFullName = a.FullName;
                var bFullName = b.FullName;
                var aNodes = aFullName.Split('.');
                var bNodes = bFullName.Split('.');
                if (aNodes.Length > bNodes.Length)
                    return 1;
                if (aNodes.Length < bNodes.Length)
                    return -1;

                return 0;
            });

            foreach (var type in sortedTypes)
            {

            }
        }
    }

}
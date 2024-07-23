using UnityEngine;
using System.IO;
using Party;
using System.Collections.Generic;
using Actors;

namespace Save
{
    public class SaveCreator
    {
        public void CreateSaveFile(int slotId)
        {
            string path = Application.persistentDataPath + "/save" + slotId + ".data"; 
            
            using(StreamWriter sw = new StreamWriter(path))
            {
                
            }
        }
    }
}

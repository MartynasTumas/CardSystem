using Newtonsoft.Json;
using Server.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server.Service
{
    public class DataAccess
    {
        private string filePath = "Codes.json";
        public List<Code> GetExistingCodes()
        {
            List<Code> existingCodes = new List<Code>();
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    existingCodes = JsonConvert.DeserializeObject<List<Code>>(json);
                }
            }
            return existingCodes;
        }

        public void WriteToFile(List<Code> codes)
        {
            var codesJson = JsonConvert.SerializeObject(codes);
            using (StreamWriter writer = new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate), Encoding.Default))
            {
                writer.Write(codesJson);
            };
        }
    }
}

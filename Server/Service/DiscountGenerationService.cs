using Newtonsoft.Json;
using Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Server.Service
{
    public class DiscountGenerationService
    {
        private DataAccess ds;

        public DiscountGenerationService()
        {
            ds = new DataAccess();
        }
        public void GenerateCodes(int amount)
        {
            List<Code> newCodes = new List<Code>();
            List<Code> existingCodes = new List<Code>();

            DataAccess ds = new DataAccess();
            existingCodes = ds.GetExistingCodes();

            for (int i = 0; i < amount; i++)
            {
                string codeValue = GetNewCode();

                if (IsUnique(codeValue, existingCodes))
                {
                    newCodes.Add(
                        new Code()
                        {
                            Products = new string[] { "Product1", "Product2" },
                            Status = CodeStatus.New,
                            Value = codeValue
                        });
                }
                else
                    i--;
            }

            if (existingCodes.Any())
            {
                newCodes.AddRange(existingCodes);
            }

            ds.WriteToFile(newCodes);
        }

        private string GetNewCode()
        {
            var bytes = new byte[4];
            using (var crypto = new RNGCryptoServiceProvider())
                crypto.GetBytes(bytes);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string newCode = Encoding.GetEncoding(1252).GetString(bytes);

            return newCode;
        }


        private bool IsUnique(string code, List<Code> existingCodes)
        {
            bool isUnique = true;

            if (!existingCodes.Any())
                return true;

            if (existingCodes.Any(x => x.Value == code))
                isUnique = false;

            return isUnique;
        }
    }
}

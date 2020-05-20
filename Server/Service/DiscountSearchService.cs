using Server.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Server.Service
{
    public class DiscountSearchService
    {
        private DataAccess ds;

        public DiscountSearchService()
        {
            ds = new DataAccess();
        }

        public Code FindCodeInFile(string codeValue)
        {
            List<Code> codes = ds.GetExistingCodes();
            Code code = new Code();

            if (codes.Any())
            {
                code = codes.Where(x => x.Value == codeValue).FirstOrDefault();
            }

            return code;
        }

        public void MarkUsedCode(Code code)
        {
            List<Code> codes = ds.GetExistingCodes();
            codes.Where(x => x.Value==code.Value).FirstOrDefault().Status = CodeStatus.Used;
            ds.WriteToFile(codes);
        }

    }
}

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.OA.Common
{
    public class WebCommon
    {

        public static List<string> PanGuSplitWord(string msg)
        {

            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(msg));
            Lucene.Net.Analysis.Token token = null;
            List<string> list = new List<string>();

            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }
            return list;
        }





       
     }
}

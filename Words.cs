using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelimeOyunu
{
    public class Words
    {
        public int WordID { get; set; }
        public string wordTextEnglish { get; set; }
        public string wordTextTurkish { get; set; }
        public int correctCount { get; set; }
        public DateTime lastCorrectDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFIconTool
{
    internal class LocalImagePathAndInfo
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private LocalImagePathAndInfo() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public LocalImagePathAndInfo(string imagePath, string imageType)
        {
            ImagePath = imagePath;
            ImageType = imageType;
        }

        public string ImagePath { get; private set; }
        public string ImageType { get; private set; }

        public override string ToString()
        {
            return "Type : " + ImageType + " | Path : " + ImagePath; 
        }
    }
}

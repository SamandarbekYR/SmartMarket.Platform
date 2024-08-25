using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Helpers
{
    public class ImageHelper
    {
        public static string MakeImageName(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            string extension = file.Extension;
            string name = "IMG_" + Guid.NewGuid() + extension;

            return name;
        }

        public static string[] GetImageExtensions()
        {
            return new string[]
            {
            //Jpg files
            ".jpg", ".jpeg",
            //Png files
            ".png",
            //Bmo files 
            ".bmp",
            //svg files 
            ".svg"
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Inazuma
{
    public class FileAbsotactionLayer : IDisposable
    {
        private bool dirty = false;
        private string fullPath = "";
        private byte[] image = new byte[0];

        public void Save()
        {
            // TBW
        }

        public void SaveAs(string newName)
        {
            // TBW
        }

        public FileAbsotactionLayer(string filename)
        {
            fullPath = Path.GetFullPath(filename);
            image = File.ReadAllBytes(fullPath);
        }

        public void Dispose()
        {
            if (dirty) Save();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        private FileBlock onlyBlock;

        public void Save()
        {
            // TBW
        }

        public void SaveAs(string newName)
        {
            // TBW
        }

        // indexの位置を含むブロックを返す。未読み込みなら読み込む
        public FileBlock GetBlock(long index)
        {
            // TBW
            if( index >= image.LongLength)
            {
                return null;
            }

            return onlyBlock;
        }

        public long Length()
        {
            // TBW
            return image.LongLength;
        }

        public FileAbsotactionLayer(string filename)
        {
            fullPath = Path.GetFullPath(filename);
            // TBW
            image = File.ReadAllBytes(fullPath);
            onlyBlock = new FileBlock(image, 0L);
        }

        public void Dispose()
        {
            if (dirty) Save();
        }
    }
    public class FileBlock
    {
        private byte[] image = new byte[0];
        private long from = 0;
        public byte[] Image { get { return image; } }
        public long From { get { return from; } }
        public FileBlock(byte[] image, long from)
        {
            this.image = image;
            this.from = from;
        }
    }
}

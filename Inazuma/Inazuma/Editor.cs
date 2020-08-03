using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Inazuma
{
    public class MyLine
    {
        private byte[] byteData;

        public override string ToString() => Encoding.UTF8.GetString(byteData);

        public MyLine(byte[] ar)
        {
            byteData = ar;
        }
    }

    public class Editor
    {
        private LinkedList<MyLine> editBuffer = new LinkedList<MyLine>();

        public MyLine GetLine(LinkedListNode<MyLine> linkedListNode, int delta)
        {
            if (delta == 0) return linkedListNode.Value;
            if (delta > 0)
            {
                for (int i = 0; i < delta; i++)
                {
                    linkedListNode = linkedListNode.Next;
                    if (linkedListNode == null) return null;
                }
                return linkedListNode.Value;
            }
            else
            {
                for (int i = 0; i < -delta; i++)
                {
                    linkedListNode = linkedListNode.Previous;
                    if (linkedListNode == null) return null;
                }
                return linkedListNode.Value;
            }
        }
        public void Load(string filename)
        {
            var ar = File.ReadAllBytes(filename);
            int p = 0;
            int bp = 0;
            for (; ; )
            {
                if (p >= ar.Length) break;
                if (ar[p++] == 0x0a)
                {
                    var line = new byte[p - bp];
                    Array.Copy(ar, bp, line, 0, p - bp);
                    editBuffer.AddLast(new MyLine(line));
                    bp = p;
                }
            }
        }

        public void Save(string filename)
        {
            // TBW
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inazuma
{
    public class VirtualVRam
    {
        private long[,] vvram;
        private FileAbsotactionLayer fal;

        public long[,] VVRam => vvram;

        public void FillVVRam(long masterPointer)
        {
            for (int x = 0; x < vvram.GetLength(0); x++)
                for (int y = 0; y < vvram.GetLength(1); y++)
                    vvram[x, y] = -1;   // fill by invalid value

            long p = masterPointer;
            var block = fal.GetBlock(p);

            int xp = 0, yp = 0;
            for (; ; )
            {
                long offset = p++ - block.From;
                if (offset >= block.Image.LongLength) break;
                long ch = block.Image[offset];
                bool newLine = false;
                if (ch == 0x0a) newLine = true;
                else if (ch == 0x0d) { /*ignore*/ }
                else vvram[xp, yp] = ch;
                xp++;
                if(newLine || xp >= vvram.GetLength(0) )
                {
                    yp++;
                    xp = 0;
                    if (yp >= vvram.GetLength(1)) break;
                }
            }
        }

        public void RecreateVRam(int xSize, int ySize, long masterPointer)
        {
            vvram = new long[xSize, ySize];
            FillVVRam(masterPointer);
        }

        public void RecreateVRam(long masterPointer)
        {
            FillVVRam(masterPointer);
        }

        public VirtualVRam(int xSize, int ySize, FileAbsotactionLayer fal)
        {
            this.fal = fal;
            vvram = new long[xSize, ySize];
            FillVVRam(0);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Data
{
    public class Animation
    {
        public string Name { get; set; }
        public int CurrentFrame { get; set; }
        public float Speed { get; set; } = 1f;

        public float Elapsed { get; set; }

        public int FrameCount
        {
            get
            {
                return _frames.Count;
            }
        }

        public bool Looped { get; set; }

        private List<Color[]> _frames = new List<Color[]>();


        public Animation(Texture2D sheet, Point frameSize)
        {

            var cols = sheet.Width / frameSize.X;
            var rows = sheet.Height / frameSize.Y;

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var frame = new Color[frameSize.X * frameSize.Y];
                    var rect = new Rectangle(x * frameSize.X, y * frameSize.Y, frameSize.X, frameSize.Y);

                    sheet.GetData(0, rect, frame, 0, frame.Length);

                    _frames.Add(frame);
                }
            }
        }

        public Color[] GetCurrentFrame()
        {
            return _frames[CurrentFrame];
        }

        public Rectangle GetCurrentRectangle()
        {

        }
    }
}

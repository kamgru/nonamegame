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
        public int FrameCount => _rectangles.Count;
        public float Speed { get; set; } = 1f;
        public float Elapsed { get; set; }
        public Rectangle CurrentRectangle => _rectangles[CurrentFrame];
        public bool Looped { get; set; }
        public Texture2D Texture2D { get; set; }

        private List<Rectangle> _rectangles = new List<Rectangle>();

        public Animation(Texture2D sheet, Point frameSize)
        {
            Texture2D = sheet;
            var cols = sheet.Width / frameSize.X;
            var rows = sheet.Height / frameSize.Y;

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    _rectangles.Add(new Rectangle(x * frameSize.X, y * frameSize.Y, frameSize.X, frameSize.Y));
                }
            }
        }
    }
}

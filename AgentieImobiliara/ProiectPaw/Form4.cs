using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    public partial class Form4 : Form
    {
        private int[] valori;
        public Form4(int[] valori)
        {
            InitializeComponent();

            Paint += Form4_Paint;
            SizeChanged += (s, e) => Invalidate();
            KeyDown += Form4_KeyDown;

            this.valori = valori;

            MouseDown += (s, e) => DoDragDrop(CreateDataObject(), DragDropEffects.Copy);
            AllowDrop = true;
            DragEnter += (s, e) =>
            {
                if (e.Data.GetDataPresent(typeof(string)))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            };
            DragDrop += (s, e) => LoadDataObject(e.Data);


        }


        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                using (PrintPreviewDialog form = new PrintPreviewDialog())
                {
                    form.Document = new DocumentGrafic(valori);
                    form.ShowDialog(this);
                }
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetDataObject(CreateDataObject());
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                LoadDataObject(Clipboard.GetDataObject());
            }
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = this.DisplayRectangle;
            r.Inflate(-20, -20);
            DesenareGrafic(valori, e.Graphics, r);
        }

        public static void DesenareGrafic(int[] valori, Graphics g, Rectangle r)
        {
            g.FillRectangle(Brushes.White, r);

            int n = valori.Length;
            float W = r.Width, H = r.Height;
            float f = 0.9f * H / (float)valori.Max();
            float w = W / n;
            for (int i = 0; i < valori.Length; i++)
            {
                float wi = w;
                float hi = valori[i] * f;
                float xi = i * w;
                float yi = H - hi;
                g.FillRectangle(Brushes.Violet, r.Left + xi, r.Top + yi, wi, hi);
                g.DrawRectangle(Pens.Black, r.Left + xi, r.Top + yi, wi, hi);
            }
            g.DrawRectangle(Pens.Black, r);

        }

        DataObject CreateDataObject()
        {
            DataObject rezultat = new DataObject();
            string text = string.Join("\t", valori);
            Bitmap imagine = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(imagine))
            {
                DesenareGrafic(valori, g, new Rectangle(0, 0, this.Width, this.Height));
            }
            rezultat.SetData(typeof(string), text);
            rezultat.SetData(typeof(Bitmap), imagine);
            return rezultat;
        }

        void LoadDataObject(IDataObject data)
        {
            if (data.GetDataPresent(typeof(string)))
            {
                string text = (string)data.GetData(typeof(string));
                valori = text.Split('\t').Select(x => int.Parse(x)).ToArray();
                Invalidate();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }



    }
}

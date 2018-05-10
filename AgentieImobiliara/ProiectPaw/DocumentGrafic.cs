using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    class DocumentGrafic : PrintDocument
    {
        int[] valori;
        int paginaCrt;
        public DocumentGrafic(int[] valori)
        {
            this.valori = valori;

        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            paginaCrt = 0;
            base.OnBeginPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            paginaCrt++;
            Form4.DesenareGrafic(valori, e.Graphics, e.MarginBounds);
            e.Graphics.DrawString(paginaCrt.ToString(), new Font(FontFamily.GenericSansSerif, 80f), Brushes.Black, e.MarginBounds.X, e.MarginBounds.Y);
            if (paginaCrt <= 2)
            {
                e.HasMorePages = true;
            }
        }
    }
}

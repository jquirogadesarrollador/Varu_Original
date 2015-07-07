using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using iTextSharp.text.pdf;
using iTextSharp.text; 

/// <summary>
/// Summary description for pdfEvents
/// </summary>
public class pdfEvents : PdfPageEventHelper
{
    // This is the contentbyte object of the writer
    PdfContentByte cb;

    // we will put the final number of pages in a template
    PdfTemplate template;

    // this is the BaseFont we are going to use for the header / footer
    BaseFont bf = null;

    #region Properties
    private Font _FooterFont;
    public Font FooterFont
    {
        get { return _FooterFont; }
        set { _FooterFont = value; }
    }

    private String _dirImagenHeader;
    public String dirImagenHeader
    {
        get {
            return _dirImagenHeader;
        }
        set {
            _dirImagenHeader = value;
        }
    }

    private DateTime _fechaImpresion;
    public DateTime fechaImpresion
    { 
        get
        {
            return _fechaImpresion;
        }
        set 
        {
            _fechaImpresion = value;
        }
    }
    private String _tipoDocumento;
    public String tipoDocumento
    {
        get
        {
            return _tipoDocumento;
        }
        set
        {
            _tipoDocumento = value;
        }
    }

    private String _versionManual;
    public String versionManual
    {
        get
        {
            return _versionManual;
        }
        set
        {
            _versionManual = value;
        }
    }

    private DateTime _fechaApartirDe;
    public DateTime  fechaApartirDe
    {
        get
        {
            return _fechaApartirDe;
        }
        set
        {
            _fechaApartirDe = value;
        }
    }
    #endregion 
    
    #region constructor
    //public pdfEvents()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
    #endregion

    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        try
        {
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            template = cb.CreateTemplate(100, 100);
        }
        catch //(DocumentException de)
        {

        }
        //catch //(System.IO.IOException ioe)
        //{

        //}
    }

    public override void OnStartPage(PdfWriter writer, Document document)
    {
        base.OnStartPage(writer, document);

        if ((tipoDocumento == "contrato") || (tipoDocumento == "contrato_preimpreso")) //no se ponen cabeceras ni fotter
        {

        }
        else
        {
            if (tipoDocumento == "clausula")
            {
                //imagen de la cabecera
                float escala = 85; //%

                Image imagenHeader = Image.GetInstance(dirImagenHeader);
                imagenHeader.ScalePercent(escala);

                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                //logo
                PdfPCell cell = new PdfPCell(imagenHeader);
                cell.Border = 0;
                cell.BorderWidth = 0;
                table.AddCell(cell);

                table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
            }
            else
            {
                if (tipoDocumento == "apertura_cuenta")
                {
                    //imagen de la cabecera
                    float escala = 70; //%

                    Image imagenHeader = Image.GetInstance(dirImagenHeader);
                    imagenHeader.ScalePercent(escala);

                    PdfPTable table = new PdfPTable(1);
                    table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                    //logo
                    PdfPCell cell = new PdfPCell(imagenHeader);
                    cell.Border = 0;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 90, writer.DirectContent);
                }
                else
                {
                    if (tipoDocumento == "autos_recomendacion")
                    {
                        //imagen de la cabecera
                        float escala = 70; //%

                        Image imagenHeader = Image.GetInstance(dirImagenHeader);
                        imagenHeader.ScalePercent(escala);

                        PdfPTable table = new PdfPTable(1);
                        table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                        //logo
                        PdfPCell cell = new PdfPCell(imagenHeader);
                        cell.Border = 0;
                        cell.BorderWidth = 0;
                        table.AddCell(cell);

                        table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                    }
                    else
                    {
                        if (tipoDocumento == "orden_compra")
                        {
                            //imagen de la cabecera
                            float escala = 70; //%

                            Image imagenHeader = Image.GetInstance(dirImagenHeader);
                            imagenHeader.ScalePercent(escala);

                            PdfPTable table = new PdfPTable(1);
                            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                            //logo
                            PdfPCell cell = new PdfPCell(imagenHeader);
                            cell.Border = 0;
                            cell.BorderWidth = 0;
                            table.AddCell(cell);

                            table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                        }
                        else
                        {
                            if (tipoDocumento == "entrevista")
                            {
                                //imagen de la cabecera
                                float escala = 85; //%

                                Image imagenHeader = Image.GetInstance(dirImagenHeader);
                                imagenHeader.ScalePercent(escala);

                                PdfPTable table = new PdfPTable(1);
                                table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                                //logo
                                PdfPCell cell = new PdfPCell(imagenHeader);
                                cell.Border = 0;
                                cell.BorderWidth = 0;
                                table.AddCell(cell);

                                table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                            }
                            else
                            {
                                if (tipoDocumento == "referencia")
                                {
                                    //imagen de la cabecera
                                    float escala = 80; //%

                                    Image imagenHeader = Image.GetInstance(dirImagenHeader);
                                    imagenHeader.ScalePercent(escala);

                                    PdfPTable table = new PdfPTable(1);
                                    table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                                    //logo
                                    PdfPCell cell = new PdfPCell(imagenHeader);
                                    cell.Border = 0;
                                    cell.BorderWidth = 0;
                                    table.AddCell(cell);

                                    table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                                }
                                else
                                {
                                    if (tipoDocumento == "afiliaciones")
                                    {
                                        //imagen de la cabecera
                                        float escala = 85; //%

                                        Image imagenHeader = Image.GetInstance(dirImagenHeader);
                                        imagenHeader.ScalePercent(escala);

                                        PdfPTable table = new PdfPTable(1);
                                        table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                                        //logo
                                        PdfPCell cell = new PdfPCell(imagenHeader);
                                        cell.Border = 0;
                                        cell.BorderWidth = 0;
                                        table.AddCell(cell);

                                        table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                                    }
                                    else
                                    {
                                        if (tipoDocumento == "manualServicio")
                                        {
                                            int pageN = writer.PageNumber;

                                            Image imagenHeader = Image.GetInstance(dirImagenHeader);
                                            imagenHeader.ScaleToFit(100, 100);
                                            //imagenHeader.ScalePercent(escala);

                                            PdfPTable table = new PdfPTable(5);
                                            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                                            Font fuente = new Font(Font.FontFamily.HELVETICA, 9);
                                            PdfPCell cell = new PdfPCell(imagenHeader);
                                            cell.Colspan = 2;
                                            cell.Padding = 3;
                                            cell.Border = 1;
                                            cell.BorderWidthBottom = 1;
                                            cell.BorderWidthLeft = 1;
                                            cell.BorderWidthRight = 1;
                                            cell.BorderWidthTop = 1;
                                            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                                            table.AddCell(cell);

                                            PdfPCell cell1 = new PdfPCell(new Phrase("PROCESO GESTIÓN COMERCIAL", fuente));
                                            cell1.Colspan = 3;
                                            cell1.Padding = 3;
                                            cell1.Border = 1;
                                            cell1.BorderWidthBottom = 1;
                                            cell1.BorderWidthLeft = 1;
                                            cell1.BorderWidthRight = 1;
                                            cell1.BorderWidthTop = 1;
                                            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                            cell1.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                                            table.AddCell(cell1);

                                            PdfPCell cell2 = new PdfPCell(new Phrase("MANUAL DE SERVICIO", fuente));
                                            cell2.Colspan = 5;
                                            cell2.Padding = 3;
                                            cell2.Border = 1;
                                            cell2.BorderWidthBottom = 1;
                                            cell2.BorderWidthLeft = 1;
                                            cell2.BorderWidthRight = 1;
                                            cell2.BorderWidthTop = 1;
                                            cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                            cell2.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                                            table.AddCell(cell2);

                                            PdfPCell cell3 = new PdfPCell(new Phrase("FECHA DE EMISIÓN\n" + DateTime.Now.ToShortDateString(), fuente));
                                            //cell3.Colspan = 5;
                                            cell3.Padding = 3;
                                            cell3.Border = 1;
                                            cell3.BorderWidthBottom = 1;
                                            cell3.BorderWidthLeft = 1;
                                            cell3.BorderWidthRight = 1;
                                            cell3.BorderWidthTop = 1;
                                            cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                            cell3.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                                            table.AddCell(cell3);

                                            PdfPCell cell4 = new PdfPCell(new Phrase("VERSION:\n" + versionManual, fuente));
                                            cell4.Colspan = 2;
                                            cell4.Padding = 3;
                                            cell4.Border = 1;
                                            cell4.BorderWidthBottom = 1;
                                            cell4.BorderWidthLeft = 1;
                                            cell4.BorderWidthRight = 1;
                                            cell4.BorderWidthTop = 1;
                                            cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                            cell4.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                                            table.AddCell(cell4);


                                            PdfPCell cell5 = new PdfPCell(new Phrase("APLICA A PARTIR:\n" + fechaApartirDe.ToShortDateString(), fuente));
                                            //cell4.Colspan = 2;
                                            cell5.Padding = 3;
                                            cell5.Border = 1;
                                            cell5.BorderWidthBottom = 1;
                                            cell5.BorderWidthLeft = 1;
                                            cell5.BorderWidthRight = 1;
                                            cell5.BorderWidthTop = 1;
                                            cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                            cell5.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                                            table.AddCell(cell5);


                                            PdfPCell cell6 = new PdfPCell(new Phrase("PAGINA " + pageN.ToString(), fuente));
                                            //cell4.Colspan = 2;
                                            cell6.Padding = 3;
                                            cell6.Border = 1;
                                            cell6.BorderWidthBottom = 1;
                                            cell6.BorderWidthLeft = 1;
                                            cell6.BorderWidthRight = 1;
                                            cell6.BorderWidthTop = 1;
                                            cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                            cell6.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                                            table.AddCell(cell6);

                                            table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                                        }
                                        else
                                        {
                                            if (tipoDocumento == "entrevista_retiro")
                                            {
                                                //imagen de la cabecera
                                                float escala = 85; //%

                                                Image imagenHeader = Image.GetInstance(dirImagenHeader);
                                                imagenHeader.ScalePercent(escala);

                                                PdfPTable table = new PdfPTable(1);
                                                table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                                                //logo
                                                PdfPCell cell = new PdfPCell(imagenHeader);
                                                cell.Border = 0;
                                                cell.BorderWidth = 0;
                                                table.AddCell(cell);

                                                table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                                            }
                                            else
                                            {
                                                if (tipoDocumento == "apertura_cuenta_avvillas")
                                                {
                                                    //imagen de la cabecera
                                                    float escala = 70; //%

                                                    Image imagenHeader = Image.GetInstance(dirImagenHeader);
                                                    imagenHeader.ScalePercent(escala);

                                                    PdfPTable table = new PdfPTable(1);
                                                    table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                                                    //logo
                                                    PdfPCell cell = new PdfPCell(imagenHeader);
                                                    cell.Border = 0;
                                                    cell.BorderWidth = 0;
                                                    table.AddCell(cell);

                                                    table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        base.OnEndPage(writer, document);

        if ((tipoDocumento == "contrato") || (tipoDocumento == "contrato_preimpreso")) //no se ponen cabeceras ni fotter
        {

        }
        else
        {
            if (tipoDocumento == "clausula")
            {
                int pageN = writer.PageNumber;
                String text = "Pag. " + pageN + " de ";
                float len = bf.GetWidthPoint(text, 8);

                Rectangle pageSize = document.PageSize;

                cb.SetRGBColorFill(100, 100, 100);

                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
                cb.ShowText(text);
                cb.EndText();

                cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                    "Impreso el: " + fechaImpresion.ToLongDateString(),
                    pageSize.GetRight(40),
                    pageSize.GetBottom(30), 0);
                cb.EndText();
            }
            else
            {
                if ((tipoDocumento == "apertura_cuenta") || (tipoDocumento == "apertura_cuenta_avvillas"))
                {

                }
                else
                {
                    if (tipoDocumento == "autos_recomendacion")
                    {

                    }
                    else
                    {
                        if (tipoDocumento == "orden_compra")
                        {
                            int pageN = writer.PageNumber;
                            String text = "Pag. " + pageN + " de ";
                            float len = bf.GetWidthPoint(text, 8);

                            Rectangle pageSize = document.PageSize;

                            cb.SetRGBColorFill(100, 100, 100);

                            cb.BeginText();
                            cb.SetFontAndSize(bf, 8);
                            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
                            cb.ShowText(text);
                            cb.EndText();

                            cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

                            cb.BeginText();
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                                "Impreso el: " + fechaImpresion.ToLongDateString(),
                                pageSize.GetRight(40),
                                pageSize.GetBottom(30), 0);
                            cb.EndText();
                        }
                        else
                        {
                            if (tipoDocumento == "manualServicio")
                            {

                            }
                            else
                            {
                                if (tipoDocumento == "entrevista_retiro")
                                {

                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
        base.OnCloseDocument(writer, document);

        template.BeginText();
        template.SetFontAndSize(bf, 8);
        template.SetTextMatrix(0, 0);
        template.ShowText("" + (writer.PageNumber - 1));
        template.EndText();
    } 
}







/*
 
 * 
 * 
 * 
 * 
 * // This is the contentbyte object of the writer
    PdfContentByte cb;

    // we will put the final number of pages in a template
    PdfTemplate template;

    // this is the BaseFont we are going to use for the header / footer
    BaseFont bf = null;

    // This keeps track of the creation time
    DateTime PrintTime = DateTime.Now;

    #region Properties
    private string _Title;
    public string Title
    {
        get { return _Title; }
        set { _Title = value; }
    }

    private string _HeaderLeft;
    public string HeaderLeft
    {
        get { return _HeaderLeft; }
        set { _HeaderLeft = value; }
    }

    private string _HeaderRight;
    public string HeaderRight
    {
        get { return _HeaderRight; }
        set { _HeaderRight = value; }
    }

    private Font _HeaderFont;
    public Font HeaderFont
    {
        get { return _HeaderFont; }
        set { _HeaderFont = value; }
    }

    private Font _FooterFont;
    public Font FooterFont
    {
        get { return _FooterFont; }
        set { _FooterFont = value; }
    }

    private String _dirImagenHeader;
    public String dirImagenHeader
    {
        get {
            return _dirImagenHeader;
        }
        set {
            _dirImagenHeader = value;
        }
    }
    #endregion 
    
    #region constructor
    //public pdfEvents()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
    #endregion
 * 
    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        try
        {
            PrintTime = DateTime.Now;
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            template = cb.CreateTemplate(100, 100);
        }
        catch (DocumentException de)
        {
        }
        catch (System.IO.IOException ioe)
        {
        }
    }

    public override void OnStartPage(PdfWriter writer, Document document)
    {
        base.OnStartPage(writer, document);

        Rectangle pageSize = document.PageSize;

        if (Title != string.Empty)
        {
            cb.BeginText();
            cb.SetFontAndSize(bf, 15);
            cb.SetRGBColorFill(50, 50, 200);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
            cb.ShowText(Title);
            cb.EndText();
        }

        if (HeaderLeft + HeaderRight != string.Empty)
        {
            PdfPTable HeaderTable = new PdfPTable(2);
            HeaderTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            HeaderTable.TotalWidth = pageSize.Width - 80;
            HeaderTable.SetWidthPercentage(new float[] { 45, 45 }, pageSize);

            PdfPCell HeaderLeftCell = new PdfPCell(new Phrase(8, HeaderLeft, HeaderFont));
            HeaderLeftCell.Padding = 5;
            HeaderLeftCell.PaddingBottom = 8;
            HeaderLeftCell.BorderWidthRight = 0;
            HeaderTable.AddCell(HeaderLeftCell);

            PdfPCell HeaderRightCell = new PdfPCell(new Phrase(8, HeaderRight, HeaderFont));
            HeaderRightCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            HeaderRightCell.Padding = 5;
            HeaderRightCell.PaddingBottom = 8;
            HeaderRightCell.BorderWidthLeft = 0;
            HeaderTable.AddCell(HeaderRightCell);

            cb.SetRGBColorFill(0, 0, 0);
            HeaderTable.WriteSelectedRows(0, -1, pageSize.GetLeft(40), pageSize.GetTop(50), cb);
        }

        //imagen 
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        base.OnEndPage(writer, document);

        int pageN = writer.PageNumber;
        String text = "Page " + pageN + " of ";
        float len = bf.GetWidthPoint(text, 8);

        Rectangle pageSize = document.PageSize;

        cb.SetRGBColorFill(100, 100, 100);

        cb.BeginText();
        cb.SetFontAndSize(bf, 8);
        cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
        cb.ShowText(text);
        cb.EndText();

        cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

        cb.BeginText();
        cb.SetFontAndSize(bf, 8);
        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
            "Printed On " + PrintTime.ToString(),
            pageSize.GetRight(40),
            pageSize.GetBottom(30), 0);
        cb.EndText();
    }

    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
        base.OnCloseDocument(writer, document);

        template.BeginText();
        template.SetFontAndSize(bf, 8);
        template.SetTextMatrix(0, 0);
        template.ShowText("" + (writer.PageNumber - 1));
        template.EndText();
    } 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using AbbigliamentoECommerceEntity;

namespace AbbigliamentoECommerceBL.Utility
{
    public static class ManagementDocument
    {
        public static FileStream CreateOrderDocument(string pPathFile, string pNumOrd, Cart pCart, User pUser)
        {

            System.IO.FileStream fs = new FileStream(pPathFile + "\\" + "Order_"+pNumOrd+".pdf", FileMode.Create);
            // Create an instance of the document class which represents the PDF document itself.  
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            var myFont = FontFactory.GetFont("Segoe UI", 18.0f, Font.BOLD);
            // Add a simple and wellknown phrase to the document in a flow layout manner  
            document.Add(new Paragraph("Abbigliamento E-Commerce"));
            Paragraph wStreetAzienda = new Paragraph("Via Benedetto Cairoli n° 1/bis");
            wStreetAzienda.Alignment = Element.ALIGN_RIGHT;
            document.Add(wStreetAzienda);
            Paragraph wCityAzienda = new Paragraph("Napoli");
            wStreetAzienda.Alignment = Element.ALIGN_RIGHT;
            document.Add(wCityAzienda);
            Paragraph wTelephoneAzienda = new Paragraph("32669987");
            wTelephoneAzienda.Alignment = Element.ALIGN_RIGHT;
            document.Add(wTelephoneAzienda);
            Paragraph wEmailAzienda = new Paragraph("info@ecommerceabbigliamenti.com");
            wEmailAzienda.Alignment = Element.ALIGN_RIGHT;
            document.Add(wEmailAzienda);
            Paragraph wTitle = new Paragraph("Order Confirmation");
            wTitle.Font = myFont;
            document.Add(wTitle);
            document.Add(new Paragraph("Ordine:" + pNumOrd + "                     Data:" + DateTime.Now.ToString()));
            document.Add(new Paragraph("Gentile " + "Arpaia" + ","));
            document.Add(new Paragraph("La ringraziamo per averci scelto."));
            document.Add(new Paragraph("Questa è conferma che abbiamo ricevuto il suo ordine definito come segue:"));

            //Lista di Prodotti
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.AddCell(getCell("Prodotto", PdfPCell.ALIGN_LEFT));
            table.AddCell(getCell("Descrizione", PdfPCell.ALIGN_CENTER));
            table.AddCell(getCell("Prezzo", PdfPCell.ALIGN_RIGHT));
            table.AddCell(getCell("Quantità", PdfPCell.ALIGN_RIGHT));
            table.AddCell(getCell("Totale", PdfPCell.ALIGN_RIGHT));
            double subtotale = 0;
            foreach (CartDetail wDetail in pCart.listProduct)
            {
                subtotale += wDetail.singleProduct.prezzo * wDetail.quantita;
                table.AddCell(getCell(wDetail.singleProduct.nome, PdfPCell.ALIGN_LEFT));
                table.AddCell(getCell(wDetail.singleProduct.descrizione, PdfPCell.ALIGN_CENTER));
                table.AddCell(getCell(wDetail.singleProduct.prezzo.ToString(), PdfPCell.ALIGN_RIGHT));
                table.AddCell(getCell(wDetail.quantita.ToString(), PdfPCell.ALIGN_RIGHT));
                table.AddCell(getCell(subtotale.ToString(), PdfPCell.ALIGN_RIGHT));
            }

            document.Add(table);

            document.Add(new Paragraph("__________________________________________________________________________"));
            document.Add(new Paragraph("Subtotale:                                            "+ subtotale.ToString()));
            document.Add(new Paragraph("IVA:                                                      5,00" ));
            subtotale += 5;
            document.Add(new Paragraph("Totale:                                               "+ subtotale.ToString()));
            // Close the document  
            document.Close();
            // Close the writer instance  
            writer.Close();
            // Always close open filehandles explicity  
            fs.Close();
            return fs;

        }

        public static PdfPCell getCell(string text, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text));
            cell.Padding = 0;
            cell.HorizontalAlignment = alignment;
            cell.Border = PdfPCell.NO_BORDER;
            return cell;
        }
    }
}
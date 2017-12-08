using Disi.Service.Formatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disi.Services.Models
{
    public class FirmaCsv : ICsvFormat
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FechaFirma { get; set; }
        public int TiempoSegIniciofirma { get; set; }
        public int TiempoSegFinFirma { get; set; }
        public Nullable<int> TiempoMiliSegInicioFirma { get; set; }
        public Nullable<int> TiempoMiliSegFinFirma { get; set; }
        public string UrlImagen { get; set; }

        public string BuildCsvHeader()
        {
            string header = "Id,Dni,FechaFirma,TiempoSegIniciofirma,TiempoSegFinFirma,TiempoMiliSegInicioFirma,TiempoMiliSegFinFirma,UrlImagen";
            return header;
        }

        public string BuildCsvItem()
        {
            string item = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
                           , CsvFormatItem.Escape(Id)
                           , CsvFormatItem.Escape(Dni)
                           , CsvFormatItem.Escape(FechaFirma)
                           , CsvFormatItem.Escape(TiempoSegIniciofirma)
                           , CsvFormatItem.Escape(TiempoSegFinFirma)
                           , CsvFormatItem.Escape(TiempoMiliSegInicioFirma)
                           , CsvFormatItem.Escape(TiempoMiliSegFinFirma)
                           , CsvFormatItem.Escape(UrlImagen));

            //PI.Net.Http.Formatting.CsvFormatter.Models.CsvFormatItem will take the value and call ToString() 
            //it will then clean the string for a CSV file removing line spaces etc
            return item;
        }
    }
}
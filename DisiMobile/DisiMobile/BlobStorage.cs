﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DisiMobile
{
    public class BlobStorage
    {
      

        public BlobStorage(){}

        public static async Task BlobStorageImage(string dni, Stream streamImage)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=disiblob;AccountKey=v8cKJJqdCxsLNmqb5oA+H0VpE5ZTejD3BKLW1GNWBXAAIv9g+kVx4Jass1e8kRnWxvEhWQ14LYVHQSA/h3bZoQ==;EndpointSuffix=core.windows.net");
            CloudBlockBlob blob = null;
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("persona");

            // Create the container if it doesn't already exist.
            if (await container.CreateIfNotExistsAsync())
            {
                await container.SetPermissionsAsync(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    }
                );
            }

            string directorioImagen = $"{dni}/{FechaHoraActual()}.png";

            blob = container.GetBlockBlobReference(directorioImagen);
   
            await blob.UploadFromStreamAsync(streamImage);

            await RegistarFirmasServices(dni, directorioImagen);
           

        }

        public static async Task RegistarFirmasServices(string dni, string directorioImagen)
        {
            Random random = new Random();

            var firma = new Firma
            {
                Dni = dni,
                FechaFirma = DateTime.Now,
                TiempoSegIniciofirma = 0,
                TiempoSegFinFirma = random.Next(1, 10),
                TiempoMiliSegInicioFirma = 0,
                TiempoMiliSegFinFirma = random.Next(1, 3600),
                UrlImagen = $"https://disiblob.blob.core.windows.net/persona/{directorioImagen}"
            };

            await RegistrarFirmas(firma);
        }

        // Create the blob client.

        public static string FechaHoraActual()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }


        public static async Task<String> RegistrarFirmas(Firma firma)
        {
            var http = new HttpClient();
            var url = "http://webapidisi.azurewebsites.net/api/Firma";
            var serializer = JsonConvert.SerializeObject(firma);
            var content = new StringContent(serializer, Encoding.UTF8, "application/json");
            var response = await http.PostAsync(url, content);
            string msg = (response.IsSuccessStatusCode) ? "Firma Registrada" : "Error al registrar";
            return msg;
        }
    }
}
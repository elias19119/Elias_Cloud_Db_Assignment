using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;

namespace UploadImage
{
    public static class UploadImage
    {
        /// <summary>
        /// Hello Frank :) Please excute UploadImage function from Postman the other function have a swagger UI
        /// </summary>
        private static QueueClient imagesqueue;
        [FunctionName("UploadImage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UploadImage")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string Connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");


            imagesqueue = new(Connection, "images-queue", new QueueClientOptions()
            {
                MessageEncoding = QueueMessageEncoding.Base64,
            });

            Stream myBlob = new MemoryStream();
            var file = req.Form.Files["File"];
            myBlob = file.OpenReadStream();
            var blobClient = new BlobContainerClient(Connection, containerName);
            var blob = blobClient.GetBlobClient(file.FileName);

            try
            {
                await blob.UploadAsync(myBlob);

                imagesqueue.SendMessage(file.FileName);

                return new OkObjectResult("file uploaded successfylly");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

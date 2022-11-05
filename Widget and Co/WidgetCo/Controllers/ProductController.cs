using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Logic.Interfaces;
using Widget_and_Co.Model.DTO;
using Microsoft.OpenApi.Models;

namespace Widget_and_Co.Controllers
{
    public class ProductController
    {
        private ILogger Logger { get; }

        private readonly IProductLogic _productlogic;

        public ProductController(ILogger<ProductController> Logger, IProductLogic productService)
        {
            this.Logger = Logger;
            _productlogic = productService;
        }

        [Function("GetAllProduct")]
        [OpenApiOperation(operationId: "GetAllProduct", tags: new[] { "products" })]
        public async Task<HttpResponseData> GetAllProduct([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products")] HttpRequestData req)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_productlogic.GetAllAsync());
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }
        [Function("GetByProductId")]
        [OpenApiOperation(operationId: "GetByProductId", tags: new[] { "products" })]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The product id parameter.")]
        public async Task<HttpResponseData> GetAProductById([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products/{productId}")] HttpRequestData req, Guid productId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_productlogic.GetByIdAsync(productId));
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("AddProduct")]
        [OpenApiOperation(operationId: "AddProduct", tags: new[] { "products" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ProductDTO), Required = true, Description = "Data for the Product that has to be created.")]
        public async Task<HttpResponseData> AddProduct([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = "products")] HttpRequestData req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ProductDTO productDTO = JsonConvert.DeserializeObject<ProductDTO>(requestBody);
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_productlogic.InsertAsync(productDTO));
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }


        [Function("UpdateProduct")]
        [OpenApiOperation(operationId: "UpdateProduct", tags: new[] { "products" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateProductDTO), Required = true, Description = "Data for the Product that has to be updated.")]
        public async Task<HttpResponseData> UpdateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = "products/{productId}")] HttpRequestData req, Guid productId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UpdateProductDTO productDTO = JsonConvert.DeserializeObject<UpdateProductDTO>(requestBody);
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_productlogic.Update(productId, productDTO));
                response.StatusCode = HttpStatusCode.Accepted;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("DeleteProduct")]
        [OpenApiOperation(operationId: "DeleteProduct", tags: new[] { "products" })]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The product id parameter.")]
        public async Task<HttpResponseData> DeleteUser([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = "products/{productId}")] HttpRequestData req, Guid productId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await _productlogic.Remove(productId);
                response.StatusCode = HttpStatusCode.Accepted;
                await response.WriteStringAsync("Product has been deleted successfully!", Encoding.UTF8);
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }
    }
}

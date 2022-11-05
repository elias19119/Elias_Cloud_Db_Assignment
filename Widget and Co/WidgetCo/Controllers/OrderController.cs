using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Widget_and_Co.Model.DTO;
using Widget_and_Co.Logic.Interfaces;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using WidgetCo.Model.DTO;
using System.Linq;
using Widget_and_Co.Logic;
using Widget_and_Co.Model;
using WidgetCo.Model.Responses;
using AutoMapper;

namespace Widget_and_Co.Controllers
{
    public class OrderController
    {
        private ILogger Logger { get; }
        private IMapper mapper;
        private readonly IOrderLogic _orderlogic;

        public OrderController(ILogger<OrderController> Logger, IOrderLogic orderService, IMapper mapper)
        {
            this.Logger = Logger;
            _orderlogic = orderService;
            this.mapper = mapper;
        }

        [Function("GetAllOrder")]
        [OpenApiOperation(operationId: "GetAllOrder", tags: new[] { "orders" })]
        public async Task<HttpResponseData> GetAllOrders([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "orders")] HttpRequestData req)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                IQueryable<Order> orders = _orderlogic.GetAllAsync();
                IQueryable<OrderResponse> orderResponses = orders.Select(u => mapper.Map<OrderResponse>(u));
                await response.WriteAsJsonAsync(orderResponses);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("GetByOrderId")]
        [OpenApiOperation(operationId: "GetByOrderId", tags: new[] { "orders" })]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The order id parameter.")]
        public async Task<HttpResponseData> GetAOrderById([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "orders/{orderId}")] HttpRequestData req, Guid OrderId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_orderlogic.GetByIdAsync(OrderId));
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("AddOrder")]
        [OpenApiOperation(operationId: "AddOrder", tags: new[] { "orders" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderDTO), Required = true, Description = "Data for the Order that has to be created.")]
        public async Task<HttpResponseData> AddOrder([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = "orders")] HttpRequestData req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            OrderDTO orderDTO = JsonConvert.DeserializeObject<OrderDTO>(requestBody);
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_orderlogic.InsertAsync(orderDTO));
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("UpdateOrder")]
        [OpenApiOperation(operationId: "UpdateOrder", tags: new[] { "orders" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateOrderDTO), Required = true, Description = "Data for the Order that has to be updated.")]
        public async Task<HttpResponseData> UpdateOrder([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = "orders/{orderId}")] HttpRequestData req, Guid orderId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UpdateOrderDTO updateOrderDTO = JsonConvert.DeserializeObject<UpdateOrderDTO>(requestBody);
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_orderlogic.Update(orderId, updateOrderDTO));
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("DeleteOrder")]
        [OpenApiOperation(operationId: "DeleteOrder", tags: new[] { "orders" })]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The order id parameter.")]
        public async Task<HttpResponseData> DeleteOrder([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = "orders/{orderId}")] HttpRequestData req, Guid orderId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await _orderlogic.Remove(orderId);
                await response.WriteStringAsync("Order has been deleted successfully!", Encoding.UTF8);
                response.StatusCode = HttpStatusCode.Accepted;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

      
        [Function("UpdateShippingTime")]
        [OpenApiOperation(operationId: "UpdateShippingTime", tags: new[] { "orders" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateShippingDataDTO), Required = true, Description = "Data for the Order that has to be updated.")]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The order id parameter.")]
        public async Task<HttpResponseData> UpdateOrdersShippingDate([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = "orders/{orderId}/updateshipping")] HttpRequestData req, Guid orderId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_orderlogic.UpdateShippingDate(orderId));
                response.StatusCode = HttpStatusCode.OK;
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


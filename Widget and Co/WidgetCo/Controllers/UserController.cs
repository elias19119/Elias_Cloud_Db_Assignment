using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Widget_and_Co.Model;
using WidgetCo.Model.Responses;
using AutoMapper;

namespace Widget_and_Co.Controllers
{
    public class UserController
    {
        private ILogger Logger { get; }
        private IMapper mapper;
        private readonly IUserLogic _userLogic;

        public UserController(ILogger<UserController> Logger, IUserLogic userService, IMapper mapper)
        {
            this.Logger = Logger;
            _userLogic = userService;
            this.mapper = mapper;
        }

        [Function("GetAllUsers")]
        [OpenApiOperation(operationId: "GetAllUsers", tags: new[] { "users" })]
        public async Task<HttpResponseData> GetAllUsers([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "users")] HttpRequestData req)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                IQueryable<User> users = _userLogic.GetAllAsync();
                IQueryable<UserResponse> userResponses = users.Select(u => mapper.Map<UserResponse>(u));
                await response.WriteAsJsonAsync(userResponses);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }
        [Function("GetByUserId")]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The user id parameter.")]
        [OpenApiOperation(operationId: "GetByUserId", tags: new[] { "users" })]
        public async Task<HttpResponseData> GetAUserById([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "users/{userId}")] HttpRequestData req, Guid userId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_userLogic.GetByIdAsync(userId));
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("AddUser")]
        [OpenApiOperation(operationId: "AddUser", tags: new[] { "users" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UserDTO), Required = true, Description = "Data for the user that has to be created.")]
        public async Task<HttpResponseData> AddUser([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = "users")] HttpRequestData req)
        {
            HttpResponseData response = req.CreateResponse();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(requestBody);

            try
            {
                await response.WriteAsJsonAsync(_userLogic.InsertAsync(userDTO));
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }


        [Function("UpdateUser")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateUserDTO), Required = true, Description = "Data for the user that has to be created.")]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The user id parameter.")]
        [OpenApiOperation(operationId: "UpdateUser", tags: new[] { "users" })]
        public async Task<HttpResponseData> UpdateUser([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = "users/{userId}")] HttpRequestData req, Guid userId)
        {
            HttpResponseData response = req.CreateResponse();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UpdateUserDTO updateUserDTO = JsonConvert.DeserializeObject<UpdateUserDTO>(requestBody);
            try
            {
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("DeleteUser")]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The user id parameter.")]
        [OpenApiOperation(operationId: "DeleteUser", tags: new[] { "users" })]
        public async Task<HttpResponseData> DeleteUser([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = "users/{userId}")] HttpRequestData req, Guid userId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                  await _userLogic.Remove(userId);
                response.StatusCode = HttpStatusCode.Accepted;
                await response.WriteStringAsync("User has been deleted successfully!", Encoding.UTF8);
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

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Widget_and_Co.Logic.Interfaces;
using Widget_and_Co.Model;
using Widget_and_Co.Model.DTO;

namespace Widget_and_Co.Controllers
{
    public class ForumController
    {
        private ILogger Logger { get; }

        private readonly IForumLogic _forumService;

        public ForumController(ILogger<ForumController> Logger, IForumLogic forumlogic)
        {
            this.Logger = Logger;
            _forumService = forumlogic;
        }

        [OpenApiOperation(operationId: "getforums", tags: new[] { "forums" })]
        [Function("GetAllForums")]
        public async Task<HttpResponseData> GetAllForums([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "forums")] HttpRequestData req)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_forumService.GetAllAsync());
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [OpenApiOperation(operationId: "getforumbyId", tags: new[] { "forums" })]
        [Function("GetByforumId")]
        [OpenApiParameter(name: "forumId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The forum id parameter.")]
        public async Task<HttpResponseData> GetByforumId([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "forums/{forumId}")] HttpRequestData req, Guid reviewId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await response.WriteAsJsonAsync(_forumService.GetByIdAsync(reviewId));

                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }


        [Function("AddForum")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ForumDTO), Required = true, Description = "Data for the Forum that has to be created.")]
        [OpenApiOperation(operationId: "AddForum", tags: new[] { "forums" })]
        public async Task<HttpResponseData> AddForum([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = "forums")] HttpRequestData req)
        {
            HttpResponseData response = req.CreateResponse();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ForumDTO reviewDTO = JsonConvert.DeserializeObject<ForumDTO>(requestBody);
            try
            {
                await response.WriteAsJsonAsync(_forumService.InsertAsync(reviewDTO));
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }


        [Function("UpdateForum")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateForumDTO), Required = true, Description = "Data for the Forum that has to be updated.")]
        [OpenApiOperation(operationId: "UpdateForum", tags: new[] { "forums" })]
        public async Task<HttpResponseData> UpdateForum([HttpTrigger(AuthorizationLevel.Anonymous, "Put", Route = "forums/{forumId}")] HttpRequestData req, Guid forumId)
        {
            HttpResponseData response = req.CreateResponse();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UpdateForumDTO updateForumDTO = JsonConvert.DeserializeObject<UpdateForumDTO>(requestBody);
            try
            {
                await response.WriteAsJsonAsync(_forumService.Update(forumId, updateForumDTO));
                response.StatusCode = HttpStatusCode.Accepted;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(e.Message, Encoding.UTF8);
            }
            return response;
        }

        [Function("DeleteForum")]
        [OpenApiParameter(name: "forumId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true, Description = "The forum id parameter.")]
        [OpenApiOperation(operationId: "DeleteForum", tags: new[] { "forums" })]
        public async Task<HttpResponseData> DeleteForum([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = "forums/{forumId}")] HttpRequestData req, Guid forumId)
        {
            HttpResponseData response = req.CreateResponse();
            try
            {
                await _forumService.Remove(forumId);
                response.StatusCode = HttpStatusCode.Accepted;
                await response.WriteStringAsync("Review has been deleted successfully!", Encoding.UTF8);
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

using System;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.Tests
{
    public class TaskClientTests
    {
        private static (TaskClient, StubMessageHandler) InstrumentedTaskClient()
        {
            (var core, var handler, _) = CoreClientTests.InstrumentedCoreClient();
            return (new TaskClient(core), handler);
        }

        [Fact]
        public async Task AddTaskTagAsync_Normal_ExecutesRequestProperly()
        {
            (var client, var handler) = InstrumentedTaskClient();

            const string exampleResponse = "{\r\n    \"success\": true,\r\n    \"data\": {\r\n        \"_id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\",\r\n        \"userId\": \"b0413351-405f-416f-8787-947ec1c85199\",\r\n        \"text\": \"Test API Params\",\r\n        \"alias\": \"test-api-params\",\r\n        \"type\": \"todo\",\r\n        \"notes\": \"\",\r\n        \"tags\": [\r\n            \"3d5d324d-a042-4d5f-872e-0553e228553e\"\r\n        ],\r\n        \"value\": -1,\r\n        \"priority\": 2,\r\n        \"attribute\": \"int\",\r\n        \"challenge\": {\r\n            \"taskId\": \"4a29874c-0308-417b-a909-2a7d262b49f6\",\r\n            \"id\": \"f23c12f2-5830-4f15-9c36-e17fd729a812\"\r\n        },\r\n        \"group\": {\r\n            \"assignedUsers\": [],\r\n            \"approval\": {\r\n                \"required\": false,\r\n                \"approved\": false,\r\n                \"requested\": false\r\n            }\r\n        },\r\n        \"reminders\": [],\r\n        \"createdAt\": \"2017-01-13T21:23:05.949Z\",\r\n        \"updatedAt\": \"2017-01-14T19:41:29.466Z\",\r\n        \"checklist\": [],\r\n        \"collapseChecklist\": false,\r\n        \"completed\": false,\r\n        \"id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\"\r\n    },\r\n    \"notifications\": []\r\n}";
            handler.Response = exampleResponse.WrapResponseBody();

            string taskId = "taskId";
            Guid guid = Guid.NewGuid();
            _ = await client.AddTaskTagAsync(taskId, guid);
            var request = handler.LastRequest;

            Assert.Equal($"/api/v3/tasks/{taskId}/tags/{guid}", request.RequestUri.AbsolutePath);
            Assert.Equal("", request.RequestUri.Query);
            Assert.Null(request.Content);
        }

        [Fact]
        public async Task AddTaskChecklistItemAsync_Normal_ExecutesRequestProperly()
        {
            (var client, var handler) = InstrumentedTaskClient();

            const string exampleResponse = "{\r\n    \"success\": true,\r\n    \"data\": {\r\n        \"_id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\",\r\n        \"userId\": \"b0413351-405f-416f-8787-947ec1c85199\",\r\n        \"text\": \"Test API Params\",\r\n        \"alias\": \"test-api-params\",\r\n        \"type\": \"todo\",\r\n        \"notes\": \"\",\r\n        \"tags\": [],\r\n        \"value\": 0,\r\n        \"priority\": 2,\r\n        \"attribute\": \"int\",\r\n        \"challenge\": {\r\n            \"taskId\": \"4a29874c-0308-417b-a909-2a7d262b49f6\",\r\n            \"id\": \"f23c12f2-5830-4f15-9c36-e17fd729a812\"\r\n        },\r\n        \"group\": {\r\n            \"assignedUsers\": [],\r\n            \"approval\": {\r\n                \"required\": false,\r\n                \"approved\": false,\r\n                \"requested\": false\r\n            }\r\n        },\r\n        \"reminders\": [],\r\n        \"createdAt\": \"2017-01-13T21:23:05.949Z\",\r\n        \"updatedAt\": \"2017-01-14T03:38:07.406Z\",\r\n        \"checklist\": [\r\n            {\r\n                \"id\": \"afe4079d-dff1-47d9-9b06-5d76c69ddb12\",\r\n                \"text\": \"Do this subtask\",\r\n                \"completed\": false\r\n            }\r\n        ],\r\n        \"collapseChecklist\": false,\r\n        \"completed\": false,\r\n        \"id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\"\r\n    },\r\n    \"notifications\": []\r\n}";
            handler.Response = exampleResponse.WrapResponseBody();

            string taskId = "taskId";
            string itemText = "itemText";
            _ = await client.AddTaskChecklistItemAsync(taskId, itemText);
            var request = handler.LastRequest;

            var body = new { text = itemText };

            Assert.Equal($"/api/v3/tasks/{taskId}/checklist", request.RequestUri.AbsolutePath);
            Assert.Equal("", request.RequestUri.Query);
            Assert.Equal(body.ToJson(), handler.LastRequestStringContent);
        }

        [Fact]
        public async Task ApproveUserTaskAsync_Normal_ExecutesRequestProperly()
        {
            (var client, var handler) = InstrumentedTaskClient();

            // TODO: Replace with real data
            const string exampleResponse = "{\r\n    \"success\": true,\r\n    \"data\": {\r\n        \"_id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\",\r\n        \"userId\": \"b0413351-405f-416f-8787-947ec1c85199\",\r\n        \"text\": \"Test API Params\",\r\n        \"alias\": \"test-api-params\",\r\n        \"type\": \"todo\",\r\n        \"notes\": \"\",\r\n        \"tags\": [],\r\n        \"value\": 0,\r\n        \"priority\": 2,\r\n        \"attribute\": \"int\",\r\n        \"challenge\": {\r\n            \"taskId\": \"4a29874c-0308-417b-a909-2a7d262b49f6\",\r\n            \"id\": \"f23c12f2-5830-4f15-9c36-e17fd729a812\"\r\n        },\r\n        \"group\": {\r\n            \"assignedUsers\": [],\r\n            \"approval\": {\r\n                \"required\": false,\r\n                \"approved\": false,\r\n                \"requested\": false\r\n            }\r\n        },\r\n        \"reminders\": [],\r\n        \"createdAt\": \"2017-01-13T21:23:05.949Z\",\r\n        \"updatedAt\": \"2017-01-14T03:38:07.406Z\",\r\n        \"checklist\": [\r\n            {\r\n                \"id\": \"afe4079d-dff1-47d9-9b06-5d76c69ddb12\",\r\n                \"text\": \"Do this subtask\",\r\n                \"completed\": false\r\n            }\r\n        ],\r\n        \"collapseChecklist\": false,\r\n        \"completed\": false,\r\n        \"id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\"\r\n    },\r\n    \"notifications\": []\r\n}";
            handler.Response = exampleResponse.WrapResponseBody();

            Guid taskId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            _ = await client.ApproveUserTaskAsync(taskId, userId);
            var request = handler.LastRequest;

            Assert.Equal($"/api/v3/tasks/{taskId}/approve/{userId}", request.RequestUri.AbsolutePath);
            Assert.Equal("", request.RequestUri.Query);
            Assert.Null(request.Content);
        }

        [Fact]
        public async Task AssignUserTaskAsync_Normal_ExecutesRequestProperly()
        {
            (var client, var handler) = InstrumentedTaskClient();

            // TODO: Replace with real data
            const string exampleResponse = "{\r\n    \"success\": true,\r\n    \"data\": {\r\n        \"_id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\",\r\n        \"userId\": \"b0413351-405f-416f-8787-947ec1c85199\",\r\n        \"text\": \"Test API Params\",\r\n        \"alias\": \"test-api-params\",\r\n        \"type\": \"todo\",\r\n        \"notes\": \"\",\r\n        \"tags\": [],\r\n        \"value\": 0,\r\n        \"priority\": 2,\r\n        \"attribute\": \"int\",\r\n        \"challenge\": {\r\n            \"taskId\": \"4a29874c-0308-417b-a909-2a7d262b49f6\",\r\n            \"id\": \"f23c12f2-5830-4f15-9c36-e17fd729a812\"\r\n        },\r\n        \"group\": {\r\n            \"assignedUsers\": [],\r\n            \"approval\": {\r\n                \"required\": false,\r\n                \"approved\": false,\r\n                \"requested\": false\r\n            }\r\n        },\r\n        \"reminders\": [],\r\n        \"createdAt\": \"2017-01-13T21:23:05.949Z\",\r\n        \"updatedAt\": \"2017-01-14T03:38:07.406Z\",\r\n        \"checklist\": [\r\n            {\r\n                \"id\": \"afe4079d-dff1-47d9-9b06-5d76c69ddb12\",\r\n                \"text\": \"Do this subtask\",\r\n                \"completed\": false\r\n            }\r\n        ],\r\n        \"collapseChecklist\": false,\r\n        \"completed\": false,\r\n        \"id\": \"84f02d6a-7b43-4818-a35c-d3336cec4880\"\r\n    },\r\n    \"notifications\": []\r\n}";
            handler.Response = exampleResponse.WrapResponseBody();

            Guid taskId = Guid.NewGuid();
            Guid assignedUserId = Guid.NewGuid();
            _ = await client.AssignUserTaskAsync(taskId, assignedUserId);
            var request = handler.LastRequest;

            Assert.Equal($"/api/v3/tasks/{taskId}/assign/{assignedUserId}", request.RequestUri.AbsolutePath);
            Assert.Equal("", request.RequestUri.Query);
            Assert.Null(request.Content);
        }

        [Fact]
        public async Task GetUserTasksAsync_NullRequest_ThrowsArgumentNullException()
        {
            (var client, var handler) = InstrumentedTaskClient();
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUserTasksAsync(null));
        }

        [Fact]
        public async Task GetUserTasksAsync_Default_ExecutesRequestProperly()
        {
            (var client, var handler) = InstrumentedTaskClient();

            const string exampleResponse = "{\"success\":true,\"data\":[{\"_id\":\"8a9d461b-f5eb-4a16-97d3-c03380c422a3\",\r\n\"userId\":\"b0413351-405f-416f-8787-947ec1c85199\",\"text\":\"15 minute break\",\r\n\"type\":\"reward\",\"notes\":\"\",\"tags\":[],\"value\":10,\"priority\":1,\"attribute\":\"str\",\r\n\"challenge\":{},\"group\":{\"assignedUsers\":[],\"approval\":{\"required\":false,\"approved\":false,\r\n\"requested\":false}},\"reminders\":[],\"createdAt\":\"2017-01-07T17:52:09.121Z\",\r\n\"updatedAt\":\"2017-01-11T14:25:32.504Z\",\"id\":\"8a9d461b-f5eb-4a16-97d3-c03380c422a3\"},\r\n,{\"_id\":\"84c2e874-a8c9-4673-bd31-d97a1a42e9a3\",\"userId\":\"b0413351-405f-416f-8787-947ec1c85199\",\r\n\"alias\":\"prac31\",\"text\":\"Practice Task 31\",\"type\":\"daily\",\"notes\":\"\",\"tags\":[],\"value\":1,\r\n\"priority\":1,\"attribute\":\"str\",\"challenge\":{},\"group\":{\"assignedUsers\":[],\r\n\"approval\":{\"required\":false,\"approved\":false,\"requested\":false}},\r\n\"reminders\":[{\"time\":\"2017-01-13T16:21:00.074Z\",\"startDate\":\"2017-01-13T16:20:00.074Z\",\r\n\"id\":\"b8b549c4-8d56-4e49-9b38-b4dcde9763b9\"}],\"createdAt\":\"2017-01-13T16:34:06.632Z\",\r\n\"updatedAt\":\"2017-01-13T16:49:35.762Z\",\"checklist\":[],\"collapseChecklist\":false,\r\n\"completed\":true,\"history\":[],\"streak\":1,\"repeat\":{\"su\":false,\"s\":false,\"f\":true,\r\n\"th\":true,\"w\":true,\"t\":true,\"m\":true},\"startDate\":\"2017-01-13T00:00:00.000Z\",\r\n\"everyX\":1,\"frequency\":\"weekly\",\"id\":\"84c2e874-a8c9-4673-bd31-d97a1a42e9a3\"}],\"notifications\":[]}";
            handler.Response = exampleResponse.WrapResponseBody();

            _ = await client.GetUserTasksAsync(new Data.Request.GetUserTasksRequest());
            var request = handler.LastRequest;

            Assert.Equal("/api/v3/tasks/user", request.RequestUri.AbsolutePath);
            Assert.Equal("", request.RequestUri.Query);
            Assert.Null(request.Content);
        }
    }
}

using System;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.Tests
{
    public class TaskClientTests
    {
        private static (TaskClient, MockMessageHandler) InstrumentedTaskClient()
        {
            (var core, var handler, _) = CoreClientTests.InstrumentedCoreClient();
            return (new TaskClient(core), handler);
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

using Habitica.NET.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.Tests.Unit.Data
{
    public class TaskClientTests
    {
        private Mock<ICoreClient> MockCoreClient = new Mock<ICoreClient>();
        private TaskClient GetTaskClient() => new TaskClient(MockCoreClient.Object);

        [Theory]
        [ClassData(typeof())]
        public void AddTaskTagAsync_InvalidParameters_ThrowsArgumentException()
        {

        }

        private class AddTaskAsyncInvalidParameters : TheoryData<string, Guid>
        {

        }


        [Fact]
        public void AddTaskTagAsync_EmptyTaskId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("taskId", () => GetTaskClient().AddTaskTagAsync(null, Guid.NewGuid()).Result);
        }

        [Fact]
        public void AddTaskTagAsync_EmptyTagId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("tagId", () => GetTaskClient().AddTaskTagAsync("taskId", default).Result);
        }

        [Fact]
        public void AddTaskChecklistItemAsync_EmptyTaskId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("taskId", () => GetTaskClient().AddTaskChecklistItemAsync(null, "itemText").Result);
        }

        [Fact]
        public void AddTaskChecklistItemAsync_EmptyItemText_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("itemText", () => GetTaskClient().AddTaskChecklistItemAsync("taskId", null).Result);
        }

        [Fact]
        public void AssignUserTaskAsync_EmptyTaskId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("taskId", () => GetTaskClient().AssignUserTaskAsync(default, Guid.NewGuid()).Result);
        }

        [Fact]
        public void AssignUserTaskAsync_EmptyAssignedUserId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>("assignedUserId", () => GetTaskClient().AssignUserTaskAsync(Guid.NewGuid(), default).Result);
        }
    }
}

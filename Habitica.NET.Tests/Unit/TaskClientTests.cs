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
        [ClassData(typeof(InvalidStringGuid))]
        public async Task AddTaskTagAsync_InvalidParameters_ThrowsArgumentException(string taskId, Guid tagId)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().AddTaskTagAsync(taskId, tagId));
        }

        private class InvalidStringGuid : TheoryData<string, Guid>
        {
            public InvalidStringGuid()
            {
                Add(null, default);
                Add(null, Guid.NewGuid());
                Add(" ", default);
                Add(" ", Guid.NewGuid());
                Add("valid", default);
            }
        }

        [Theory]
        [ClassData(typeof(InvalidStringString))]
        public async Task AddTaskChecklistItemAsync_InvalidParameters_ThrowsArgumentException(string taskId, string itemText)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().AddTaskChecklistItemAsync(taskId, itemText));
        }

        private class InvalidStringString : TheoryData<string, string>
        {
            public InvalidStringString()
            {
                Add(null, null);
                Add(null, "valid");
                Add(" ", null);
                Add(" ", "valid");
                Add("valid", null);
                Add("valid", " ");
            }
        }

        [Theory]
        [ClassData(typeof(InvalidGuidGuid))]
        public async Task ApproveUserTaskAsync_InvalidParameters_ThrowsArgumentException(Guid taskId, Guid userId)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().ApproveUserTaskAsync(taskId, userId));
        }

        private class InvalidGuidGuid : TheoryData<Guid, Guid>
        {
            public InvalidGuidGuid()
            {
                Add(default, default);
                Add(default, Guid.NewGuid());
                Add(Guid.NewGuid(), default);
            }
        }

        [Theory]
        [ClassData(typeof(InvalidGuidGuid))]
        public async Task AssignUserTaskAsync_InvalidParameters_ThrowsArgumentException(Guid taskId, Guid assignedUserId)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().AssignUserTaskAsync(taskId, assignedUserId));
        }

    }
}

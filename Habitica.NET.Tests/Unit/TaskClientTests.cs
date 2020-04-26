using Habitica.NET.Data.Request;
using Habitica.NET.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using HTask = Habitica.NET.Data.Model.Task;

namespace Habitica.NET.Tests.Unit.Data
{
    public class TaskClientTests
    {
        private Mock<ICoreClient> MockCoreClient = new Mock<ICoreClient>();
        private TaskClient GetTaskClient() => new TaskClient(MockCoreClient.Object);

        [Theory]
        [ClassData(typeof(InvalidStringGuid))]
        public Task AddTaskTagAsync_InvalidParameters_ThrowsArgumentException(string taskId, Guid tagId)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().AddTaskTagAsync(taskId, tagId));
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
        public Task AddTaskChecklistItemAsync_InvalidParameters_ThrowsArgumentException(string taskId, string itemText)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().AddTaskChecklistItemAsync(taskId, itemText));
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
        public Task ApproveUserTaskAsync_InvalidParameters_ThrowsArgumentException(Guid taskId, Guid userId)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().ApproveUserTaskAsync(taskId, userId));
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
        public Task AssignUserTaskAsync_InvalidParameters_ThrowsArgumentException(Guid taskId, Guid userId)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().AssignUserTaskAsync(taskId, userId));
        }


        [Theory]
        [ClassData(typeof(InvalidGuidIEnumerableT<CreateTask>))]
        public Task CreateChallengeTaskAsync_InvalidParameters_ThrowsArgumentException(Guid taskId, IEnumerable<CreateTask> tasks)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().CreateChallengeTaskAsync(taskId, tasks));
        }

        private class InvalidGuidIEnumerableT<T> : TheoryData<Guid, IEnumerable<T>>
            where T: new()
        {
            public InvalidGuidIEnumerableT()
            {
                Add(default, new List<T> { new T() });
                Add(Guid.NewGuid(), null);
                Add(Guid.NewGuid(), new List<T> { });
            }
        }

        [Theory]
        [ClassData(typeof(InvalidGuidIEnumerableT<CreateTask>))]
        public Task CreateGroupTaskAsync_InvalidParameters_ThrowsArgumentException(Guid taskId, IEnumerable<CreateTask> tasks)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().CreateGroupTaskAsync(taskId, tasks));
        }

        [Theory]
        [ClassData(typeof(InvalidIEnumerableT<CreateUserTask>))]
        public Task CreateUserTaskAsync_InvalidParameters_ThrowsArgumentException(IEnumerable<CreateUserTask> tasks)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().CreateUserTaskAsync(tasks));
        }

        private class InvalidIEnumerableT<T> : TheoryData<IEnumerable<T>>
            where T : new()
        {
            public InvalidIEnumerableT()
            {
                Add(null);
                Add(new List<T> { });
            }
        }

        [Theory]
        [ClassData(typeof(InvalidStringGuid))]
        public Task DeleteTaskChecklistItemAsync_InvalidParameters_ThrowsArgumentException(string taskId, Guid itemId)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().DeleteTaskChecklistItemAsync(taskId, itemId));
        }

        [Theory]
        [ClassData(typeof(InvalidStringGuid))]
        public Task DeleteTaskTagAsync_InvalidParameters_ThrowsArgumentException(string taskId, Guid tag)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().DeleteTaskTagAsync(taskId, tag));
        }

        [Theory]
        [ClassData(typeof(InvalidString))]
        public Task DeleteTaskAsync_InvalidParameters_ThrowsArgumentException(string taskId)
        {
            return Assert.ThrowsAsync<ArgumentException>(() => GetTaskClient().DeleteTaskAsync(taskId));
        }

        private class InvalidString : TheoryData<string>
        {
            public InvalidString()
            {
                Add(null);
                Add("");
                Add(" ");
            }
        }
    }
}

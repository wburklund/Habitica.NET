// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Request;
using Habitica.NET.Data.Response;
using Habitica.NET.Interfaces;
using Habitica.NET.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Habitica.NET
{
    public class TaskClient
    {
        private readonly ICoreClient client;

        public TaskClient(ICoreClient client)
        {
            this.client = client;
        }

        // TODO: Add field-level task validation

        /// <summary>
        /// Adds a tag to the given task.
        /// </summary>
        /// <param name="taskId">The task ID or alias.</param>
        /// <param name="tagId">The tag ID.</param>
        /// <returns>The updated task.</returns>
        public Task<GetTask> AddTaskTagAsync(string taskId, Guid tagId)
        {
            if (string.IsNullOrWhiteSpace(taskId)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(taskId));
            if (tagId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(tagId));
            return AddTaskTagInternalAsync(taskId, tagId);
        }
        private async Task<GetTask> AddTaskTagInternalAsync(string taskId, Guid tagId)
        {
            string path = $"/api/v3/tasks/{taskId}/tags/{tagId}";
            var response = await client.PostAsync(path.ToUri(), null).Safe();
            return response.UnwrapHabiticaResponse<GetTask>();
        }

        /// <summary>
        /// Adds an item to the given task's checklist.
        /// </summary>
        /// <param name="taskId">The task ID or alias.</param>
        /// <param name="itemText">The text of the checklist item.</param>
        /// <returns>The updated task.</returns>
        public Task<GetTask> AddTaskChecklistItemAsync(string taskId, string itemText)
        {
            if (string.IsNullOrWhiteSpace(taskId)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(taskId));
            if (string.IsNullOrWhiteSpace(itemText)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(itemText));
            return AddTaskChecklistItemInternalAsync(taskId, itemText);
        }
        private async Task<GetTask> AddTaskChecklistItemInternalAsync(string taskId, string itemText)
        {
            string path = $"/api/v3/tasks/{taskId}/checklist";
            var body = new { text = itemText };
            using (var content = new StringContent(body.ToJson()))
            {
                var response = await client.PostAsync(path.ToUri(), content).Safe();
                return response.UnwrapHabiticaResponse<GetTask>();
            }
        }

        /// <summary>
        /// Approves a user assigned to a group task.
        /// </summary>
        /// <param name="taskId">The task ID.</param>
        /// <param name="userId">The ID of the user that will be approved.</param>
        /// <returns>The approved task.</returns>
        public Task<GetTask> ApproveUserTaskAsync(Guid taskId, Guid userId)
        {
            if (taskId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(taskId));
            if (userId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(userId));
            return ApproveUserTaskInternalAsync(taskId, userId);
        }
        private async Task<GetTask> ApproveUserTaskInternalAsync(Guid taskId, Guid userId)
        {
            string path = $"/api/v3/tasks/{taskId}/approve/{userId}";
            var response = await client.PostAsync(path.ToUri(), null).Safe();
            return response.UnwrapHabiticaResponse<GetTask>();
        }

        /// <summary>
        /// Assigns a group task to a user.
        /// </summary>
        /// <param name="taskId">The task ID.</param>
        /// <param name="assignedUserId">The ID of the user that will be assigned to the task.</param>
        /// <returns>The assigned task.</returns>
        public Task<GetTask> AssignUserTaskAsync(Guid taskId, Guid assignedUserId)
        {
            if (taskId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(taskId));
            if (assignedUserId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(assignedUserId));
            return AssignUserTaskInternalAsync(taskId, assignedUserId);
        }
        private async Task<GetTask> AssignUserTaskInternalAsync(Guid taskId, Guid assignedUserId)
        {
            string path = $"/api/v3/tasks/{taskId}/assign/{assignedUserId}";
            var response = await client.PostAsync(path.ToUri(), null).Safe();
            return response.UnwrapHabiticaResponse<GetTask>();
        }

        /// <summary>
        /// Creates new tasks belonging to a challenge.
        /// </summary>
        /// <param name="challengeId">The challenge the new tasks will be created for.</param>
        /// <param name="tasks">The tasks that will be created.</param>
        /// <returns>An object representing the created tasks.</returns>
        public Task<IEnumerable<GetTask>> CreateChallengeTaskAsync(Guid challengeId, IEnumerable<CreateTask> tasks)
        {
            if (challengeId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(challengeId));
            if (tasks == null || !tasks.Any()) throw new ArgumentException(Resources.ExceptionEmptyCollection, nameof(tasks));
            return CreateChallengeTaskInternalAsync(challengeId, tasks);
        }
        private async Task<IEnumerable<GetTask>> CreateChallengeTaskInternalAsync(Guid challengeId, IEnumerable<CreateTask> tasks)
        {
            string path = $"/api/v3/tasks/challenge/{challengeId}";
            using (var content = new StringContent(tasks.ToJson()))
            {
                var response = await client.PostAsync(path.ToUri(), content).Safe();

                if (tasks.Count() > 1) return response.UnwrapHabiticaResponse<IEnumerable<GetTask>>();

                return new List<GetTask> { response.UnwrapHabiticaResponse<GetTask>() };
            }
        }

        /// <summary>
        /// Creates new tasks belonging to a group.
        /// </summary>
        /// <param name="groupId">The group the new tasks will be created for.</param>
        /// <param name="tasks">The tasks that will be created.</param>
        /// <returns>An object representing the created tasks.</returns>
        public Task<IEnumerable<GetTask>> CreateGroupTaskAsync(Guid groupId, IEnumerable<CreateTask> tasks)
        {
            if (groupId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(groupId));
            if (tasks == null || !tasks.Any()) throw new ArgumentException(Resources.ExceptionEmptyCollection, nameof(tasks));
            return CreateGroupTaskInternalAsync(groupId, tasks);
        }
        private async Task<IEnumerable<GetTask>> CreateGroupTaskInternalAsync(Guid groupId, IEnumerable<CreateTask> tasks)
        {
            string path = $"/api/v3/tasks/group/{groupId}";
            using (var content = new StringContent(tasks.ToJson()))
            {
                var response = await client.PostAsync(path.ToUri(), content).Safe();

                if (tasks.Count() > 1) return response.UnwrapHabiticaResponse<IEnumerable<GetTask>>();

                return new List<GetTask> { response.UnwrapHabiticaResponse<GetTask>() };
            }

        }

        /// <summary>
        /// Creates new tasks belonging to the user.
        /// </summary>
        /// <param name="tasks">The tasks that will be created.</param>
        /// <returns>An object representing the created tasks.</returns>
        public Task<IEnumerable<GetTask>> CreateUserTaskAsync(IEnumerable<CreateUserTask> tasks)
        {
            if (tasks == null || !tasks.Any()) throw new ArgumentException(Resources.ExceptionEmptyCollection, nameof(tasks));
            return CreateUserTaskInternalAsync(tasks);
        }
        private async Task<IEnumerable<GetTask>> CreateUserTaskInternalAsync(IEnumerable<CreateUserTask> tasks)
        {
            string path = $"/api/v3/tasks/user";
            using (var content = new StringContent(tasks.ToJson()))
            {
                var response = await client.PostAsync(path.ToUri(), content).Safe();

                if (tasks.Count() > 1) return response.UnwrapHabiticaResponse<IEnumerable<GetTask>>();

                return new List<GetTask> { response.UnwrapHabiticaResponse<GetTask>() };
            }

        }

        /// <summary>
        /// Deletes a checklist item belonging to a task.
        /// </summary>
        /// <param name="taskId">The task ID or alias.</param>
        /// <param name="itemId">The ID of the checklist item.</param>
        /// <returns>An asynchronous operation.</returns>
        public Task DeleteTaskChecklistItemAsync(string taskId, Guid itemId)
        {
            if (string.IsNullOrWhiteSpace(taskId)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(taskId));
            if (itemId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(itemId));

            string path = $"/api/v3/tasks/{taskId}/checklist/{itemId}";
            return client.DeleteAsync(path.ToUri());
        }

        /// <summary>
        /// Deletes a tag belonging to a task.
        /// </summary>
        /// <param name="taskId">The task ID or alias.</param>
        /// <param name="tag">The ID of the tag.</param>
        /// <returns>An asynchronous operation.</returns>
        public Task DeleteTaskTagAsync(string taskId, Guid tag)
        {
            if (string.IsNullOrWhiteSpace(taskId)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(taskId));
            if (tag == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(tag));

            string path = $"/api/v3/tasks/{taskId}/tags/{tag}";
            return client.DeleteAsync(path.ToUri());
        }

        /// <summary>
        /// Deletes a task.
        /// </summary>
        /// <param name="taskId">The task ID or alias.</param>
        /// <returns>An asynchronous operation.</returns>
        public Task DeleteTaskAsync(string taskId)
        {
            if (string.IsNullOrWhiteSpace(taskId)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(taskId));

            string path = $"/api/v3/tasks/{taskId}";
            return client.DeleteAsync(path.ToUri());
        }

        /// <summary>
        /// Clears the user's completed todos.
        /// </summary>
        /// <returns>An asynchronous operation.</returns>
        public Task ClearCompletedTodosAsync()
        {
            const string path = "/api/v3/tasks/clearCompletedTodos";
            return client.PostAsync(path.ToUri(), null);
        }

        /// <summary>
        /// Gets the user's tasks that match given request parameters.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>The user's matching tasks.</returns>
        public Task<IEnumerable<GetTask>> GetUserTasksAsync(GetUserTasksRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return GetUserTasksInternalAsync(request);
        }
        private async Task<IEnumerable<GetTask>> GetUserTasksInternalAsync(GetUserTasksRequest request)
        {
            const string endpoint = "/api/v3/tasks/user";

            NameValueCollection queryParameters = new NameValueCollection();
            if (request.TaskType.HasValue) queryParameters.Add("type", request.TaskType.Value.ToString("G"));
            if (request.DueDate.HasValue) queryParameters.Add("dueDate", request.DueDate.Value.ToString("o", DateTimeFormatInfo.InvariantInfo));

            string path = endpoint + queryParameters.ToQueryString();
            Uri uri = path.ToUri();
            var res = await client.GetAsync(uri).Safe();

            return res.UnwrapHabiticaResponse<IEnumerable<GetTask>>();
        }
    }
}

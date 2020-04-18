﻿// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Enum;
using Habitica.NET.Data.Request;
using Habitica.NET.Interfaces;
using Habitica.NET.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Habitica.NET
{
    public class TaskClient
    {
        private readonly IHttpClient client;

        public TaskClient(IHttpClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Adds a tag to the given task.
        /// </summary>
        /// <param name="taskId">The task ID or alias.</param>
        /// <param name="tagId">The tag ID.</param>
        /// <returns>The updated task.</returns>
        public Task<Data.Model.Task> AddTaskTagAsync(string taskId, Guid tagId)
        {
            if (string.IsNullOrWhiteSpace(taskId)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(taskId));
            if (tagId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(tagId));
            return AddTaskTagInternalAsync(taskId, tagId);
        }
        private async Task<Data.Model.Task> AddTaskTagInternalAsync(string taskId, Guid tagId)
        {
            string path = $"/api/v3/tasks/{taskId}/tags/{tagId}";
            var response = await client.PostAsync(path.ToUri(), null).Safe();
            return response.UnwrapHabiticaResponse<Data.Model.Task>();
        }

        /// <summary>
        /// Adds an item to the given task's checklist.
        /// </summary>
        /// <param name="taskId">The task ID or alias.</param>
        /// <param name="itemText">The text of the checklist item.</param>
        /// <returns>The updated task.</returns>
        public Task<Data.Model.Task> AddTaskChecklistItemAsync(string taskId, string itemText)
        {
            if (string.IsNullOrWhiteSpace(taskId)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(taskId));
            if (string.IsNullOrWhiteSpace(itemText)) throw new ArgumentException(Resources.ExceptionWhitespaceString, nameof(itemText));
            return AddTaskChecklistItemInternalAsync(taskId, itemText);
        }
        private async Task<Data.Model.Task> AddTaskChecklistItemInternalAsync(string taskId, string itemText)
        {
            string path = $"/api/v3/tasks/{taskId}/checklist";
            var body = new { text = itemText };
            using (var content = new StringContent(body.ToJson()))
            {
                var response = await client.PostAsync(path.ToUri(), content).Safe();
                return response.UnwrapHabiticaResponse<Data.Model.Task>();
            }
        }

        /// <summary>
        /// Approves a user assigned to a group task.
        /// </summary>
        /// <param name="taskId">The task ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>The approved task.</returns>
        public Task<Data.Model.Task> ApproveUserTaskAsync(Guid taskId, Guid userId)
        {
            if (taskId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(taskId));
            if (userId == default) throw new ArgumentException(Resources.ExceptionDefault, nameof(userId));
            return ApproveUserTaskInternalAsync(taskId, userId);
        }
        private async Task<Data.Model.Task> ApproveUserTaskInternalAsync(Guid taskId, Guid userId)
        {
            string path = $"/api/v3/tasks/{taskId}/approve/{userId}";
            var response = await client.PostAsync(path.ToUri(), null).Safe();
            return response.UnwrapHabiticaResponse<Data.Model.Task>();
        }

        public Task AssignUserTaskAsync(Guid taskId, Guid assignedUserId)
        {
            return AssignUserTaskInternalAsync(taskId, assignedUserId);
        }
        private async Task AssignUserTaskInternalAsync(Guid taskId, Guid assignedUserId)
        {

        }

        public Task CreateChallengeTaskAsync(Guid challengeId, IEnumerable<Data.Model.Task> tasks)
        {
            return CreateChallengeTaskInternalAsync(challengeId, tasks);
        }
        private async Task CreateChallengeTaskInternalAsync(Guid challengeId, IEnumerable<Data.Model.Task> tasks)
        {

        }

        public Task CreateGroupTaskAsync(Guid groupId, IEnumerable<Data.Model.Task> tasks)
        {
            return CreateGroupTaskInternalAsync(groupId, tasks);
        }
        private async Task CreateGroupTaskInternalAsync(Guid groupId, IEnumerable<Data.Model.Task> tasks)
        {

        }

        public Task CreateUserTaskAsync(IEnumerable<Data.Model.Task> tasks)
        {
            return CreateUserTaskInternalAsync(tasks);
        }
        private async Task CreateUserTaskInternalAsync(IEnumerable<Data.Model.Task> tasks)
        {

        }

        public Task DeleteTaskChecklistItemAsync(string taskId, string itemText)
        {
            return DeleteTaskChecklistItemInternalAsync(taskId, itemText);
        }
        private async Task DeleteTaskChecklistItemInternalAsync(string taskId, string itemText)
        {

        }

        public Task DeleteTaskTagAsync(string taskId, Guid tag)
        {
            return DeleteTaskTagInternalAsync(taskId, tag);
        }
        private async Task DeleteTaskTagInternalAsync(string taskId, Guid tag)
        {

        }

        public Task DeleteTaskAsync(string taskId)
        {
            return DeleteTaskInternalAsync(taskId);
        }
        private async Task DeleteTaskInternalAsync(string taskId)
        {

        }

        public async Task ClearCompletedTodosAsync()
        {

        }

        public Task GetChallegeTasksAsync(Guid challengeId)
        {
            return GetChallegeTasksInternalAsync(challengeId);
        }
        private async Task GetChallegeTasksInternalAsync(Guid challengeId)
        {

        }

        public Task GetGroupApprovalsAsync(Guid groupId)
        {
            return GetGroupApprovalsInternalAsync(groupId);
        }
        private async Task GetGroupApprovalsInternalAsync(Guid groupId)
        {

        }

        public Task GetGroupTasksAsync(Guid groupId, TaskListType? type)
        {
            return GetGroupTasksInternalAsync(groupId, type.Value.ToString());
        }
        private async Task GetGroupTasksInternalAsync(Guid groupId, string type)
        {

        }

        public Task GetTaskAsync(string taskId)
        {
            return GetTaskInternalAsync(taskId);
        }
        private async Task GetTaskInternalAsync(string taskId)
        {

        }

        public Task<IEnumerable<Data.Model.Task>> GetUserTasksAsync(GetUserTasksRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return GetUserTasksInternalAsync(request);
        }
        private async Task<IEnumerable<Data.Model.Task>> GetUserTasksInternalAsync(GetUserTasksRequest request)
        {
            const string endpoint = "/api/v3/tasks/user";

            NameValueCollection queryParameters = new NameValueCollection();
            if (request.TaskType.HasValue) queryParameters.Add("type", request.TaskType.Value.ToString("G"));
            if (request.DueDate.HasValue) queryParameters.Add("dueDate", request.DueDate.Value.ToString("o", DateTimeFormatInfo.InvariantInfo));

            string path = endpoint + queryParameters.ToQueryString();
            Uri uri = path.ToUri();
            var res = await client.GetAsync(uri).Safe();

            return res.UnwrapHabiticaResponse<IEnumerable<Data.Model.Task>>();
        }
    }
}

﻿// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Enum;
using Habitica.NET.Data.Request;
using Habitica.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
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

        public Task AddTaskTagAsync(Guid task, Guid tag)
        {
            return AddTaskTagInternalAsync(task, tag);
        }
        private async Task AddTaskTagInternalAsync(Guid task, Guid tag)
        { 

        }

        public Task AddTaskChecklistItemAsync(Guid taskId, string itemText)
        {
            return AddTaskChecklistItemInternalAsync(taskId.ToString(), itemText);
        }
        public Task AddTaskChecklistItemAsync(string taskAlias, string itemText)
        {
            return AddTaskChecklistItemInternalAsync(taskAlias, itemText);
        }
        private async Task AddTaskChecklistItemInternalAsync(string taskId, string itemText)
        {

        }

        public Task ApproveUserTaskAsync(Guid taskId, Guid userId)
        {
            return ApproveUserTaskInternalAsync(taskId, userId);
        }
        private async Task ApproveUserTaskInternalAsync(Guid taskId, Guid userId)
        {

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
            var body = await res.Content.ReadAsStringAsync().Safe();

            return body.UnwrapHabiticaResponse<IEnumerable<Data.Model.Task>>();
        }
    }
}

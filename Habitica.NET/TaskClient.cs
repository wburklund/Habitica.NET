// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

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

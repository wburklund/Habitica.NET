using Habitica.NET.Data.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Habitica.NET.Tests.Unit.Data.Request
{
    public class CreateUserTaskTests
    {
        [Fact]
        public void GettersAndSetters_Invoked_StoreAndReturnData()
        {
            var Tags = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            var request = new CreateUserTask()
            {
                Tags = Tags
            };

            Assert.Equal(Tags, request.Tags);
        }

    }
}

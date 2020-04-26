using Habitica.NET.Data.Enum;
using Habitica.NET.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Habitica.NET.Tests.Unit.Data.Model
{
    public class TaskTests
    {
        [Fact]
        public void GettersAndSetters_Invoked_StoreAndReturnData()
        {
            const CharacterAttribute Attribute = CharacterAttribute.INT;
            const bool ByHabitica = true;
            const object Challenge = null;
            Guid ChallengeLeaderId = Guid.NewGuid();
            DateTime CreatedAt = DateTime.Now;
            const object Group = null;
            Guid Id = Guid.NewGuid();
            const string Notes = "Notes";
            const double Priority = 1.0;
            IEnumerable<Reminder> Reminders = new List<Reminder>();
            var Tags = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            const string Text = "Text";
            const string Type = "Type";
            DateTime UpdatedAt = DateTime.Now;
            Guid UserId = Guid.NewGuid();
            const double Value = 350.0;

            var request = new Task
            {
                Attribute = Attribute,
                ByHabitica = ByHabitica,
                Challenge = Challenge,
                ChallengeLeaderId = ChallengeLeaderId,
                CreatedAt = CreatedAt,
                Group = Group,
                Id = Id,
                Notes = Notes,
                Priority = Priority,
                Reminders = Reminders,
                Tags = Tags,
                Text = Text,
                Type = Type,
                UpdatedAt = UpdatedAt,
                UserId = UserId,
                Value = Value
            };

            Assert.Equal(Attribute, request.Attribute);
            Assert.Equal(ByHabitica, request.ByHabitica);
            Assert.Equal(Challenge, request.Challenge);
            Assert.Equal(ChallengeLeaderId, request.ChallengeLeaderId);
            Assert.Equal(CreatedAt, request.CreatedAt);
            Assert.Equal(Group, request.Group);
            Assert.Equal(Id, request.Id);
            Assert.Equal(Notes, request.Notes);
            Assert.Equal(Priority, request.Priority);
            Assert.Equal(Reminders, request.Reminders);
            Assert.Equal(Tags, request.Tags);
            Assert.Equal(Text, request.Text);
            Assert.Equal(Type, request.Type);
            Assert.Equal(UpdatedAt, request.UpdatedAt);
            Assert.Equal(UserId, request.UserId);
            Assert.Equal(Value, request.Value);
        }

    }
}

using Habitica.NET.Data.Model;
using Habitica.NET.Data.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Habitica.NET.Tests.Unit.Data.Request
{
    public class CreateTaskTests
    {

        [Fact]
        public void GettersAndSetters_Invoked_StoreAndReturnData()
        {
            const string Text = "Text";
            const string Type = "Type";
            const string Alias = "Alias";
            const string Attribute = "Attribute";
            const bool CollapseChecklist = true;
            const string Notes = "Notes";
            const string Date = "Date";
            const double Priority = 1.0;
            IEnumerable<Reminder> Reminders = new List<Reminder>();
            const string Frequency = "Frequency";
            const string Repeat = "Repeat";
            const int EveryX = 42;
            const int Streak = 17;
            IEnumerable<int> DaysOfMonth = new List<int> { 1, 2, 3 };
            IEnumerable<int> WeeksOfMonth = new List<int> { 4, 5, 6 };
            const bool Up = true;
            const bool Down = true;
            const double Value = 350.0;

            var request = new CreateTask()
            {
                Text = Text,
                Type = Type,
                Alias = Alias,
                Attribute = Attribute,
                CollapseChecklist = CollapseChecklist,
                Notes = Notes,
                Date = Date,
                Priority = Priority,
                Reminders = Reminders,
                Frequency = Frequency,
                Repeat = Repeat,
                EveryX = EveryX,
                Streak = Streak,
                DaysOfMonth = DaysOfMonth,
                WeeksOfMonth = WeeksOfMonth,
                Up = Up,
                Down = Down,
                Value = Value
            };

            Assert.Equal(Text, request.Text);
            Assert.Equal(Type, request.Type);
            Assert.Equal(Alias, request.Alias);
            Assert.Equal(Attribute, request.Attribute);
            Assert.Equal(CollapseChecklist, request.CollapseChecklist);
            Assert.Equal(Notes, request.Notes);
            Assert.Equal(Date, request.Date);
            Assert.Equal(Priority, request.Priority);
            Assert.Equal(Reminders, request.Reminders);
            Assert.Equal(Frequency, request.Frequency);
            Assert.Equal(Repeat, request.Repeat);
            Assert.Equal(EveryX, request.EveryX);
            Assert.Equal(Streak, request.Streak);
            Assert.Equal(DaysOfMonth, request.DaysOfMonth);
            Assert.Equal(WeeksOfMonth, request.WeeksOfMonth);
            Assert.Equal(Up, request.Up);
            Assert.Equal(Down, request.Down);
            Assert.Equal(Value, request.Value);
        }
    }
}

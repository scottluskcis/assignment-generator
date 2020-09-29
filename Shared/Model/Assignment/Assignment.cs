using System;
using System.Collections.Generic;
using BlazorApp.Shared.Model.Question;

namespace BlazorApp.Shared.Model.Assignment
{
    public class Assignment
    {
        public string CreatorId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public IEnumerable<IQuestion> Questions { get; set; }
    }
}
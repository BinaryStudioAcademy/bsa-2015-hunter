using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services.Dto
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int? ProfileId { get; set; }
        public string Text { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Edited { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
    }
}

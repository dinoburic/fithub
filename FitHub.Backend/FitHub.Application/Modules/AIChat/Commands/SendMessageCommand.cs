using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.AIChat.Commands
{
    public class SendMessageCommand : IRequest<SendMessageResponse>
    {
        public string Message { get; set; }
        public List<ChatMessageDto> History { get; set; }
    }

    public class ChatMessageDto
    {
        public string Role { get; set; } // "user" ili "assistant"
        public string Content { get; set; }
    }
}

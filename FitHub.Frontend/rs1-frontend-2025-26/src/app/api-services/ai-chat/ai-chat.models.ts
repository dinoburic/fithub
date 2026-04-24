export interface ChatMessageDto {
  role: string; // 'user' ili 'assistant'
  content: string;
}

export interface SendMessageCommand {
  message: string;
  history: ChatMessageDto[]; // Dodano novo polje
}
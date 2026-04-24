import { Component, inject } from '@angular/core';
import { AIChatService } from '../../../../api-services/ai-chat/ai-chat.service';
import { ToasterService } from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-ai-chat',
  standalone: false,
  templateUrl: './ai-chat.component.html',
  styleUrl: './ai-chat.component.scss',
})
export class AiChatComponent {
  userMessage: string = '';
  chatHistory: { sender: string, text: string }[] = [];
  isLoading: boolean = false;

  private toaster = inject(ToasterService);

  constructor(private chatService: AIChatService) {}

  sendMessage() {
    if (!this.userMessage.trim()) return;

    const messageToSend = this.userMessage;

    // 1. Map current history for backend (before adding a new message)
    const mappedHistory = this.chatHistory
      .filter(msg => msg.sender !== 'System') // Do not send error output to the bot
      .map(msg => ({
        role: msg.sender === 'Korisnik' ? 'user' : 'assistant',
        content: msg.text
      }));

    // 2. Prepare payload expected by backend
    const payload = {
      message: messageToSend,
      history: mappedHistory
    };

    // 3. Update UI
    this.chatHistory.push({ sender: 'Korisnik', text: messageToSend });
    this.userMessage = ''; 
    this.isLoading = true;

    // 4. Send request to server
    this.chatService.sendMessage(payload).subscribe({
      next: (response) => {
        this.chatHistory.push({ sender: 'FitHub AI', text: response.reply });
        this.isLoading = false;
      },
      error: (err) => {
        this.toaster.error("Error communicating with AI");
        this.chatHistory.push({ sender: 'System', text: 'An error occurred.' });
        this.isLoading = false;
      }
    });
  }
}

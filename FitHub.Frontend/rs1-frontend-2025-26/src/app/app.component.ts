import { Component, inject, OnInit, signal } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToasterService } from './core/services/toaster.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  protected readonly title = signal('rs1-frontend-2025-26');
  currentLang: string = 'bs';
  private toaster = inject(ToasterService);

  constructor(private translate: TranslateService) {
   

    // Initialize translation service
    this.translate.addLangs(['en', 'bs']);
    this.translate.setDefaultLang('bs');

    // Load language from localStorage or use default
    const savedLang = localStorage.getItem('language') || 'bs';
    this.currentLang = savedLang;

    this.translate.use(savedLang).subscribe({
      next: (translations) => {
       
      },
      error: (error) => {
         this.toaster.error('Error loading translations:', error);
         this.toaster.error('Check if files exist at: /i18n/' + savedLang + '.json');
      }
    });
  }

  ngOnInit(): void {
    // Test translation
    this.translate.get('PRODUCTS.TITLE').subscribe((res: string) => {
       
      if (res === 'PRODUCTS.TITLE') {
         this.toaster.error('⚠️ Translation not working! Key returned instead of value.');
         this.toaster.error('Possible causes:');
         this.toaster.error('1. Translation files not in /i18n/ folder');
         this.toaster.error('2. JSON files have syntax errors');
         this.toaster.error('3. TranslateService not properly initialized');
      }
    });
  }

  switchLanguage(lang: string): void {
    this.currentLang = lang;
    localStorage.setItem('language', lang);
    this.translate.use(lang).subscribe({
      next: () => {
         
      },
      error: (error) => {
         this.toaster.error('Error switching language:', error);
      }
    });
  }
}

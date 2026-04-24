import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-success-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './order-success-page.component.html',
  styleUrl: './order-success-page.component.scss'
})
export class OrderSuccessPageComponent implements OnInit {
  private route = inject(ActivatedRoute);
  
  orderId: string | null = '';

  ngOnInit(): void {
    this.orderId = this.route.snapshot.paramMap.get('id');
  }
}
import { CurrencyPipe, NgFor } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgFor, CurrencyPipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  memberships = [
    { name: 'Monthly Plan', duration: '1 month', price: 500, icon: 'fa-calendar-day' },
    { name: 'Quarterly Plan', duration: '3 months', price: 1500, icon: 'fa-calendar-alt' },
    { name: 'Half-Yearly Plan', duration: '6 months', price: 3000, icon: 'fa-calendar-check' },
    { name: 'Yearly Plan', duration: '12 months', price: 6000, icon: 'fa-calendar' }
  ];
}

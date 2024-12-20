import { CurrencyPipe, NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { MembershipPlan } from '../_models/membership-plan';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgFor, CurrencyPipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  memberships: MembershipPlan[] = [
    { name: 'Monthly Plan', duration: '1 month', price: 500,},
    { name: 'Quarterly Plan', duration: '3 months', price: 1500,},
    { name: 'Half-Yearly Plan', duration: '6 months', price: 3000,},
    { name: 'Yearly Plan', duration: '12 months', price: 6000,}
  ];
}

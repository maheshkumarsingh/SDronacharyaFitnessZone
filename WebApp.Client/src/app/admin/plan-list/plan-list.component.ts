import { Component, inject, OnInit } from '@angular/core';
import { MembershipService } from '../../_services/membership.service';
import { MembershipPlan } from '../../_models/membership-plan';
import { CommonModule, CurrencyPipe, NgFor } from '@angular/common';

@Component({
  selector: 'app-plan-list',
  standalone: true,
  imports: [CurrencyPipe, NgFor, CommonModule],
  templateUrl: './plan-list.component.html',
  styleUrl: './plan-list.component.css'
})
export class PlanListComponent implements OnInit{
  membershipService = inject(MembershipService);
  membershipPlans: MembershipPlan[] = [];


  ngOnInit(): void {
    this.membershipService.getMembershipPlans();
    this.membershipPlans = this.membershipService.membershipPlans();
  }
}

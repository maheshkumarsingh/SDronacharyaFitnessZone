import { Component, inject, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { Membership } from '../../_models/membership';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrencyPipe, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { MemberService } from '../../_services/member.service';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [DatePipe,NgIf, NgFor, NgClass, CurrencyPipe],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private memberService = inject(MemberService);
  member?: Member;
  memberships: Membership[] = [];
  memberLoginName: string|null = null;
  totalDueAmount :number =0;
  totalPaidAmount :number =0;
  
  
  ngOnInit(): void {
    this.memberLoginName = this.route.snapshot.paramMap.get('memberLoginName'); //while routeLink we have passed memberLoginName
    if (this.memberLoginName) {
      this.fetchMemberDetails(this.memberLoginName);
    }
  }

  fetchMemberDetails(loginName: string): void {
    this.memberService.getMemberByMemberLoginName(loginName).subscribe({
      next: (member) => {
        this.member = member;
        this.memberships = member.memberships || [];
        this.calculateAmounts();
      },
      error: (err) => {
        console.error('Error fetching member details:', err);
      }
    });
  }

  getMembershipType(type: number): string {
    switch (type) {
      case 0:
        return 'Monthly';
      case 1:
        return 'Quarterly';
      case 2:
        return 'Half-Yearly';
      case 3:
        return 'Yearly';
      default:
        return 'Unknown';
    }
  }
  calculateAmounts():void{
    let sumForDue = 0;
    let sumForPaid = 0;
    if(this.memberships.length>0)
    {
      for (let index = 0; index < this.memberships.length; index++) {
        sumForDue += this.memberships[index].dueAmount;
        sumForPaid += this.memberships[index].paidAmount;
      }
      this.totalDueAmount = sumForDue;
      this.totalPaidAmount = sumForPaid;
    }
  }
}

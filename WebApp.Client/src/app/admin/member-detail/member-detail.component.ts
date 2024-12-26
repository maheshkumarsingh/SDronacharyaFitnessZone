import { Component, effect, inject, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { Membership } from '../../_models/membership';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CurrencyPipe, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { MemberService } from '../../_services/member.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { SupplementOrder } from '../../_models/SupplementOrder';
import { MembershipCreateComponent } from "../membership-create/membership-create.component";
import { MembershipService } from '../../_services/membership.service';
import { MemberEditComponent } from "../member-edit/member-edit.component";
import { MembershipEditComponent } from "../membership-edit/membership-edit.component";

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [DatePipe, NgIf, NgFor, NgClass, CurrencyPipe, TabsModule, GalleryModule, RouterLink, MembershipCreateComponent, MemberEditComponent, MembershipEditComponent],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  public membershipService = inject(MembershipService);
  member?: Member;
  memberships: Membership[] = [];
  selectedMembership: Membership|null = null;
  memberLoginName: string | null = null;
  totalDueAmount: number = 0;
  totalPaidAmount: number = 0;
  images: GalleryItem[] = [];
  supplementOrdered: SupplementOrder[] = [];
  isCreateMembership: boolean = false;
  isEditMembership: boolean = false;
  constructor() {
    effect(() => {
      this.memberships = this.membershipService.memberships();
      this.calculateAmounts();
    });
  }
  ngOnInit(): void {
    this.memberLoginName = this.route.snapshot.paramMap.get('memberLoginName');
    if (this.memberLoginName) {
      this.fetchMemberMemberships(this.memberLoginName);
    }
    this.isCreateMembership = false;
  }
  fetchMemberMemberships(memberLoginName : string): void {
    this.membershipService.getMemberships(memberLoginName).subscribe({
      next: _ => {
        this.memberships = this.membershipService.memberships();
        if(this.memberships.length > 0){
        this.calculateAmounts();
        }
      },
      error: (err) => {
        console.error('Error fetching member memberships:', err);
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
  calculateAmounts(): void {
    let sumForDue = 0;
    let sumForPaid = 0;
    if (this.memberships.length > 0) {
      for (let index = 0; index < this.memberships.length; index++) {
        sumForDue += this.memberships[index].dueAmount;
        sumForPaid += this.memberships[index].paidAmount;
      }
      this.totalDueAmount = sumForDue;
      this.totalPaidAmount = sumForPaid;
    }
  }
  createMembership(): void {
    this.isCreateMembership = true;
  }
  onCancelCreate(event : boolean): void {
    this.isCreateMembership = event;
  }
  onEditMembership(membership : Membership): void {
    this.selectedMembership = membership;
    this.isEditMembership = true;
    console.log('Selected Membership: ', this.selectedMembership);
    console.log('isEditMembership: ', this.isEditMembership);
  }
  onCancelEdit(event : boolean): void {
    this.isEditMembership = event;
  }
}

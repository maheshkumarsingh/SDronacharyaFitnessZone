import { Component, inject, input, OnInit, output } from '@angular/core';
import { Membership } from '../../_models/membership';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MembershipService } from '../../_services/membership.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-membership-edit',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './membership-edit.component.html',
  styleUrl: './membership-edit.component.css'
})
export class MembershipEditComponent implements OnInit {
  membership = input.required<Membership>();
  isEditMembership = output<boolean>();
  membershipService = inject(MembershipService);
  toastr = inject(ToastrService);
  membershipType: string = '';
  repayAmount: number = 0;
  ngOnInit(): void {
    this.repayAmount = this.membership().dueAmount;
  }
  updateMembership(): void {
    this.membership().paidAmount += this.repayAmount;
    this.membershipService.updateMembership(this.membership()).subscribe({
      next: (updatedMembership) => {
        this.toastr.success('Membership updated successfully');
      },
      error: (error) => {
        this.toastr.error('Failed to update membership');
      }
    });
    this.isEditMembership.emit(false);
  }
  onEditCancel(): void {
    this.isEditMembership.emit(false);
  }
  getMembershipType(value: number) : string{
    switch (value) {
      case 0: this.membershipType = 'Monthly'; break;
      case 1: this.membershipType = 'Quarterly'; break;
      case 2: this.membershipType = 'Half_Yearly'; break;
      case 3: this.membershipType = 'Yearly'; break;
      default: this.membershipType = 'Monthly'; break;
    }
    return this.membershipType;
  }
}

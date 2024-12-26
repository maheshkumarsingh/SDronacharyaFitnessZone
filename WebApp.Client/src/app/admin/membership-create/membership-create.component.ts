import { Component, inject, input, OnInit, output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePickerComponent } from "../../_forms/date-picker/date-picker.component";
import { Member } from '../../_models/member';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { MembershipService } from '../../_services/membership.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-membership-create',
  standalone: true,
  imports: [ReactiveFormsModule, DatePickerComponent, NgIf],
  templateUrl: './membership-create.component.html',
  styleUrl: './membership-create.component.css'
})
export class MembershipCreateComponent implements OnInit {
  memberLoginName = input.required<string>();
  //member:Member|any = input.required<Member>();
  isCreateMembership = output<boolean>();
  newMembershipPlanForm: FormGroup = new FormGroup({});
  maxDate = new Date();
  minDate = new Date();
  dueAmount: number = 0;
  amount: number = 0;
  membershipService = inject(MembershipService);
  toastr = inject(ToastrService);
  router = inject(Router);


  ngOnInit(): void {
    this.initializeCreateMembershipForm();
    // Set minDate to January 1st of this year
    this.minDate = new Date(new Date().getFullYear(), 0, 1);
    // Set maxDate to December 31st of this year
    this.maxDate = new Date(new Date().getFullYear(), 11, 31);
    this.dueAmount = 0;
    this.amount = 0;
    //this.memberLoginName = this.member().memberLoginName;
  }

  initializeCreateMembershipForm() {
    this.newMembershipPlanForm = new FormGroup({
      memberLoginName: new FormControl(this.memberLoginName(), [Validators.required]),
      membershipType: new FormControl("", [Validators.required]),
      paidAmount: new FormControl(0, [Validators.required,Validators.min(0), Validators.max(this.amount)]),
      membershipStartDate: new FormControl('', [Validators.required]),
      isOldMember: new FormControl('', [Validators.required]),
    });
  }
  createMembershipPlan() {
    const msd = this.getDateOnly(this.newMembershipPlanForm.get('membershipStartDate')?.value);
    this.newMembershipPlanForm.patchValue({membershipStartDate: msd});
    const type:number = Number(this.newMembershipPlanForm.get('membershipType')?.value);
    this.newMembershipPlanForm.patchValue({membershipType: type});
    this.newMembershipPlanForm.patchValue({memberLoginName: this.memberLoginName()});
    console.log(this.newMembershipPlanForm.value);
    this.membershipService.createMembership(this.newMembershipPlanForm.value).subscribe({
      next: (response) => {
        //console.log(response);
        this.toastr.success('Membership Plan Created Successfully');
        this.isCreateMembership.emit(false);
        //this.router.navigateByUrl('/member-detail/'+this.memberLoginName);
        //this.router.navigate(['/member-detail',this.memberLoginName]);
      },
      error: (error) => {
        console.log(error);
        this.toastr.error('Membership Plan not created');
      }
    });
  }
  getAmount():number {
    if (this.newMembershipPlanForm.get('membershipType')?.value !="") {
      const type:number = Number(this.newMembershipPlanForm.get('membershipType')?.value);
      switch (type) {
        case 0:
          this.amount = 500;
          break;
        case 1:
          this.amount = 1500;
          break;
        case 2:
          this.amount = 3000;
          break;
        case 3:
          this.amount = 6000;
          break;
        default:
          this.amount = 0;
          break;
      }
    }
    this.dueAmount = this.amount;
    return this.amount;
  }
  getDueAmount() :number{
    //console.log(this.newMembershipPlanForm.get('paidAmount')?.value);
    if(this.newMembershipPlanForm.get('paidAmount')?.value){
      this.dueAmount = this.amount - this.newMembershipPlanForm.get('paidAmount')?.value;
    }
    return this.dueAmount;
  }
  private getDateOnly(dob:string| undefined){
    if(!dob) return;
    return new Date(dob).toISOString().slice(0,10);
  }
  onCancel(){
    this.isCreateMembership.emit(false);
  }
  validateAmount(event: any): void {
    const value = +event.target.value;
    const maxAmount = this.getAmount();

    if (value < 0) {
      this.newMembershipPlanForm.get('paidAmount')?.setValue(0);
    } else if (value > maxAmount) {
      this.newMembershipPlanForm.get('paidAmount')?.setValue(maxAmount);
    }
  }
}

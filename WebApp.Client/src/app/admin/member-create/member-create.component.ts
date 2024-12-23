import { JsonPipe, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TextInputComponent } from "../../_forms/text-input/text-input.component";
import { DatePickerComponent } from "../../_forms/date-picker/date-picker.component";
import { MemberService } from '../../_services/member.service';

@Component({
  selector: 'app-member-create',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, DatePickerComponent],
  templateUrl: './member-create.component.html',
  styleUrl: './member-create.component.css'
})
export class MemberCreateComponent implements OnInit {
  createMemberForm: FormGroup = new FormGroup({})
  private fb = inject(FormBuilder);
  private http = inject(HttpClient);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  maxdate = new Date();
  memberLoginName: string = '';
  memberService = inject(MemberService);
  validationErrors: string[] | undefined;
  ngOnInit(): void {
    this.initializeForm();
    this.maxdate.setFullYear(this.maxdate.getFullYear() - 15);
  }
  initializeForm() {
    this.createMemberForm = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.pattern(/^[A-Za-z]+$/)]),
      middleName: new FormControl('', [Validators.pattern(/^[A-Za-z]+$/)]),
      lastName: new FormControl('', [Validators.pattern(/^[A-Za-z]+$/)]),
      dateOfBirth: new FormControl('', [Validators.required]),
      gender: new FormControl(0, [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]),
      confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')]),
      phoneNumber: new FormControl('', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]),
      alternatePhoneNumber: new FormControl('', [Validators.pattern(/^[0-9]{10}$/)]),
      address: new FormControl('', [Validators.required,]),
      bloodGroup: new FormControl('', [Validators.required,]),
      joiningDate: new FormControl('', [Validators.required,]),
      isOldmember: new FormControl('', [Validators.required,]),
    });

    // this.createMemberForm = this.fb.group({
    //   firstName: ['', Validators.required],
    //   middleName: [''],
    //   lastName: ['', Validators.required],
    //   dateOfBirth: ['', Validators.required],
    //   gender: [0, Validators.required],
    //   email: ['', [Validators.required, Validators.email]],
    //   password: ['', Validators.required],
    //   confirmPassword: ['', Validators.required],
    //   phoneNumber: [''],
    //   alternatePhoneNumber: [''],
    //   address: [''],
    //   bloodGroup: [''],
    //   membershipType: [0, Validators.required],
    //   joiningDate: ['', Validators.required],
    //   isOldmember: [false],
    //   membershipStartDate: ['', Validators.required],
    // });


  }
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    }
  }
  onSubmit() {
    const dob = this.getDateOnly(this.createMemberForm.get('dateOfBirth')?.value);
    const jd = this.getDateOnly(this.createMemberForm.get('joiningDate')?.value);
    this.createMemberForm.patchValue({ dateOfBirth: dob });
    this.createMemberForm.patchValue({ joiningDate: jd });
    this.memberService.createMember(this.createMemberForm.value).subscribe({
      next: _ =>{
        this.toastr.success('Member Created Successfully');
        this.router.navigateByUrl('/admin-dashboard');
      },
      error: error =>{
        this.toastr.error('Error while creating member');
        this.validationErrors = error;
        console.log('validationsErrors  '+ this.validationErrors);
      }
    })
  }
  onCancel() {
    this.toastr.warning('All Changes will be lost. Navigating to home');
    this.router.navigateByUrl('/admin-dashboard');
  }
  showMemberLoginName(): string | any {
    if (this.createMemberForm.get('email')?.value) {
      return this.memberLoginName = this.createMemberForm.get('email')?.value.split('@')[0];
    }
    return "";
  }
  getMinimumDate(): Date {
    const minDate = new Date();
    minDate.setFullYear(2020);
    return minDate;
  }
  checkOldMember(): boolean {
    const joiningDate = this.createMemberForm.get('joiningDate')?.value;
    if (joiningDate) {
      const parsedDate = new Date(joiningDate); // Convert to Date object
      if (!isNaN(parsedDate.getTime())) { // Ensure it's a valid date
        return parsedDate.getFullYear() < 2023;
      }
    }
    return false;
  }
  private getDateOnly(dob:string| undefined){
    if(!dob) return;
    return new Date(dob).toISOString().slice(0,10);
  }
}

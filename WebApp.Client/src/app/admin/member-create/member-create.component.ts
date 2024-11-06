import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-member-create',
  standalone: true,
  imports: [ ReactiveFormsModule],
  templateUrl: './member-create.component.html',
  styleUrl: './member-create.component.css'
})
export class MemberCreateComponent {
  memberForm: FormGroup | any;
  private fb = inject(FormBuilder);
  private http = inject(HttpClient);

    setMember()
    {
      this.memberForm = this.fb.group({
        firstName: ['', Validators.required],
        middleName: [''],
        lastName: ['', Validators.required],
        dateOfBirth: ['', Validators.required],
        gender: [0, Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required],
        phoneNumber: [''],
        alternatePhoneNumber: [''],
        address: [''],
        bloodGroup: [''],
        membershipType: [0, Validators.required],
        joiningDate: ['', Validators.required],
        isOldmember: [false],
        membershipStartDate: ['', Validators.required],
      });
    }

  onSubmit() {
    if (this.memberForm?.valid) {
      const memberData = this.memberForm.value;
      this.http.post('https://localhost:7221/api/Members', memberData)
        .subscribe({
          next: (response) => console.log('Member created successfully:', response),
          error: (error) => console.error('Error creating member:', error)
        });
    }
  }
}

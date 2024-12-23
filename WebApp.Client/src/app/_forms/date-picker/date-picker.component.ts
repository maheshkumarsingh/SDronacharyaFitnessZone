import { NgIf } from '@angular/common';
import { Component, input, Self, Inject } from '@angular/core';
import { ControlValueAccessor, Form, FormControl, FormsModule, NgControl, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerConfig, BsDatepickerModule} from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-picker',
  standalone: true,
  imports: [BsDatepickerModule, ReactiveFormsModule, NgIf],
  templateUrl: './date-picker.component.html',
  styleUrl: './date-picker.component.css'
})
export class DatePickerComponent implements ControlValueAccessor{
  readonly label = input<string>();
  maxDate = input<Date>();
  minDate = input<Date>();
  bsConfig?: Partial<BsDatepickerConfig>;

  constructor(@Self() @Inject(NgControl) public ngControl: NgControl) { 
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-dark-blue',
      dateInputFormat: 'DD MMM YYYY',
    }
  }
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }
}

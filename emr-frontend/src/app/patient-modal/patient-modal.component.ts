import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PatientService } from '../services/patients.service';
import { Patient } from '../interfaces/patient';

@Component({
  selector: 'app-patient-modal',
  templateUrl: './patient-modal.component.html',
  styleUrls: ['./patient-modal.component.css']
})
export class PatientModalComponent implements OnInit {
  form: FormGroup;

  constructor(public dialogRef: MatDialogRef<PatientModalComponent>, public fb: FormBuilder,
    private patientService: PatientService) {
    this.form = this.fb.group({
      name: this.fb.group({
        f_name: [''],
        l_name: ['']
      }),
      dob: this.fb.group({
        year: [null],
        month: [null],
        day: [null]
      }),
      SSN: [''],
      phone_no: [''],
      address: [''],
      sex: ['']
    })
  }

  ngOnInit(): void {
  }

  submitForm() {
    var newPatient: Patient = {ssn: this.form.get("SSN")!.value, phone_no: this.form.get("phone_no")!.value,
                              address: this.form.get("address")!.value, sex: this.form.get("sex")!.value,
                              name: {f_name: this.form.get("name.f_name")!.value, l_name: this.form.get("name.l_name")!.value},
                              dob: {year: this.form.get("dob.year")!.value, month: this.form.get("dob.month")!.value, day: this.form.get("dob.day")!.value}};


    this.patientService.addPatient(newPatient).subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
    this.closeModal();
  }

  closeModal() {
    this.dialogRef.close();
  }

}

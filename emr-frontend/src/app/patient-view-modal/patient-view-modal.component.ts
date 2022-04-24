import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Inject } from '@angular/core';
import { PatientService } from '../services/patients.service';
import { Patient } from '../interfaces/patient';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EMRRecord } from '../interfaces/emrRecord';

@Component({
  selector: 'app-patient-view-modal',
  templateUrl: './patient-view-modal.component.html',
  styleUrls: ['./patient-view-modal.component.css']
})
export class PatientViewModalComponent implements OnInit {
  form: FormGroup;
  patient: Patient;

  constructor(public dialogRef: MatDialogRef<PatientViewModalComponent>, public fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any, private patientService: PatientService) {
    this.patient = {ssn: 0, address: "", sex: "", phone_no: 0, name: {f_name: "", l_name: ""},
                    dob: {year: 0, month: 0, day: 0}, emr: {p_ssn: 0, emr_id: "", record: []}};
    this.form = this.fb.group({
      prescription: this.fb.group({
        mname: [''],
        dosage: ['']
      }),
      diagnosis: this.fb.group({
        date: this.fb.group({
          year: [null],
          month: [null],
          day: [null]
        }),
        condition: ['']
      }),
      symptom: this.fb.group({
        description: [''],
        body_area: [''],
        observer_SSN: ['']
      })
    });
  }

  ngOnInit(): void {
    this.getPatient(this.data.SSN);
  }

  getPatient(ssn: string): void {
    this.patientService.getPatient(ssn).subscribe(
      (resp) => this.patient = (resp.data as Patient)
    );
  }

  submitForm(): void {
    var newRecord: EMRRecord = {symptom: {description: this.form.get("symptom.description")!.value, body_area: this.form.get("symptom.body_area")!.value, observer_SSN: window.sessionStorage.getItem("userSSN")!},
                                diagnosis: {condition: this.form.get("diagnosis.condition")!.value,
                                date: {year: this.form.get("diagnosis.date.year")!.value, month: this.form.get("diagnosis.date.month")!.value, day: this.form.get("diagnosis.date.day")!.value}},
                                prescription: {mname: this.form.get("prescription.mname")!.value, dosage: this.form.get("prescription.dosage")!.value}};

    this.patientService.addDiagnosis(this.data.SSN, newRecord).subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
  }

  closeModal(): void {
    this.dialogRef.close();
  }
}

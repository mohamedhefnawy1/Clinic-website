import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MedicationService } from '../services/medications.service';
import { Medication } from '../interfaces/medication';

@Component({
  selector: 'app-medication-modal',
  templateUrl: './medication-search-modal.component.html',
  styleUrls: ['./medication-search-modal.component.css']
})
export class MedicationSearchModalComponent implements OnInit {
  form: FormGroup;
  medicationList: Medication[];

  constructor(public dialogRef: MatDialogRef<MedicationSearchModalComponent>, public fb: FormBuilder,
    private medicationService: MedicationService) {
    this.form = this.fb.group({
      symptom: ['']
    })
    this.medicationList = [];
  }

  ngOnInit(): void {
  }

  submitForm() {

    this.medicationService.searchMedication(this.form.get("symptom")!.value).subscribe(
      (response) => this.medicationList = response.data as Medication[],
      (error) => console.log(error)
    );

    this.ngOnInit();
  }

  closeModal() {
    this.dialogRef.close();
  }

}

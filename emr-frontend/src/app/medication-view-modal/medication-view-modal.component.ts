import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MedicationService } from '../services/medications.service';
import { Medication } from '../interfaces/medication';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Inject } from '@angular/core';


@Component({
  selector: 'app-medication-view-modal',
  templateUrl: './medication-view-modal.component.html',
  styleUrls: ['./medication-view-modal.component.css']
})
export class MedicationViewModalComponent implements OnInit {
  medication: Medication

  constructor(public dialogRef: MatDialogRef<MedicationViewModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private medicationService: MedicationService) { 
      
      this.medication = {m_name: '', side_effects: '', treats: ['']};

  }

  ngOnInit(): void {
    this.getMedication(this.data.m_name);
  }

  getMedication(m_name: string): void{
    this.medicationService.getMedication(m_name).subscribe(
      (resp) => this.medication = (resp.data as Medication)
    );
  }

  closeModal(): void{
    this.dialogRef.close();
  }

}

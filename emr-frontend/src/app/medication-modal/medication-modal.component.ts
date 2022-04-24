import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MedicationService } from '../services/medications.service';
import { Medication } from '../interfaces/medication';

@Component({
  selector: 'app-medication-modal',
  templateUrl: './medication-modal.component.html',
  styleUrls: ['./medication-modal.component.css']
})
export class MedicationModalComponent implements OnInit {
  form: FormGroup;

  constructor(public dialogRef: MatDialogRef<MedicationModalComponent>, public fb: FormBuilder,
    private medicationService: MedicationService) {
    this.form = this.fb.group({
      m_name: [''],
      side_effects: [''],
      treats: ['']
    })
  }

  ngOnInit(): void {
  }

  parseTreats(x: string): object[] {
    var treats: object[] = [];
    var y = x.split(";");
    for (let i = 0; i < y.length; i++) {
      var res = y[i].split(",");
      var treats_ = {
        symptom: res[0],
        body_area: res[1]
      }
      treats.push(treats_);
    }
    return treats;
  }
  
  submitForm() {

    var newMedication: Medication = {
      m_name: String(this.form.get("m_name")!.value),
      side_effects: String(this.form.get("side_effects")!.value),
      treats: this.parseTreats(String(this.form.get("treats")!.value)),
    };


    this.medicationService.addMedication(newMedication).subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
    this.closeModal();
  }

  closeModal() {
    this.dialogRef.close();
  }

}

import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { PatientModalComponent } from '../patient-modal/patient-modal.component';
import { PatientViewModalComponent } from '../patient-view-modal/patient-view-modal.component';
import { PatientService } from '../services/patients.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  items: string[] = [];
  pageOfItems: Array<string> = new Array<string>();

  constructor(private patientService: PatientService, public matDialog: MatDialog) { }

  ngOnInit(): void {
    this.getPatients();
  }

  getPatients(): void {
    this.patientService.getPatients()
    .subscribe(patients => this.items = (patients.data as string[]));
  }

  onChangePage(pageOfItems: Array<string>) {
    this.pageOfItems = pageOfItems;
  }

  openModal() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.id = "patient-modal-component";
    dialogConfig.height = "350px";
    dialogConfig.width = "600px";
    const modalDialog = this.matDialog.open(PatientModalComponent, dialogConfig);
  }

  openView(ssn: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.id = "patient-modal-component";
    dialogConfig.height = "350px";
    dialogConfig.width = "600px";
    dialogConfig.data = {
      SSN: ssn
    };
    const modalDialog = this.matDialog.open(PatientViewModalComponent, dialogConfig);
  }

}

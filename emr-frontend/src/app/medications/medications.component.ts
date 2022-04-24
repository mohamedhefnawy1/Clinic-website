import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MedicationModalComponent } from '../medication-modal/medication-modal.component';
import { MedicationSearchModalComponent } from '../medication-search-modal/medication-search-modal.component';
import { MedicationService } from '../services/medications.service';
import { MedicationViewModalComponent } from '../medication-view-modal/medication-view-modal.component';

@Component({
  selector: 'app-medications',
  templateUrl: './medications.component.html',
  styleUrls: ['./medications.component.css']
})
export class MedicationsComponent implements OnInit {
  items: string[] = [];
  pageOfItems: Array<string> = new Array<string>();

  constructor(private medicationService: MedicationService, public matDialog: MatDialog) { }

  ngOnInit(): void {
    this.getMedications();
  }

  getMedications(): void {
    this.medicationService.getMedications()
    .subscribe(medications => this.items = (medications.data as string[]));
  }

  onChangePage(pageOfItems: Array<string>) {
    this.pageOfItems = pageOfItems;
  }

  openModal() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.id = "medication-modal-component";
    dialogConfig.height = "350px";
    dialogConfig.width = "600px";
    const modalDialog = this.matDialog.open(MedicationModalComponent, dialogConfig);
  }
    openView(m_name: string) {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.id = "medication-view-modal-component";
        dialogConfig.height = "350px";
        dialogConfig.width = "600px";
        dialogConfig.data = {
            m_name: m_name
        };
        const modalDialog = this.matDialog.open(MedicationViewModalComponent, dialogConfig)
    }

  openSearch() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.id = "medication-search-modal-component";
    dialogConfig.height = "350px";
    dialogConfig.width = "600px";
    const modalDialog = this.matDialog.open(MedicationSearchModalComponent, dialogConfig);

  }

}

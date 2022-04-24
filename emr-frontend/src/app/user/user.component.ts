import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DoctorModalComponent } from '../doctor-modal/doctor-modal.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  userSSN: string = "None";

  constructor(public matDialog: MatDialog) {
  }

  ngOnInit(): void {
    var SSN = window.sessionStorage.getItem("userSSN");
    if (typeof(SSN) == 'string') {
      this.userSSN = SSN;
    }
  }

  openModal() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.id = "patient-modal-component";
    dialogConfig.height = "350px";
    dialogConfig.width = "600px";
    dialogConfig.data = {
      SSN: this.userSSN
    };
    const modalDialog = this.matDialog.open(DoctorModalComponent, dialogConfig);
  }

}

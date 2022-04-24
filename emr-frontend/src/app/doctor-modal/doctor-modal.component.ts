import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Doctor } from '../interfaces/doctor';
import { DoctorService } from '../services/doctors.service';

@Component({
  selector: 'app-doctor-modal',
  templateUrl: './doctor-modal.component.html',
  styleUrls: ['./doctor-modal.component.css']
})
export class DoctorModalComponent implements OnInit {
  doctor: Doctor;
  doctorList: string[];

  constructor(public dialogRef: MatDialogRef<DoctorModalComponent>, public fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any, private doctorService: DoctorService) {
      this.doctor = {ssn: 0, address: "", sex: "", phone_no: 0, name: {f_name: "", l_name: ""},
                    dob: {year: 0, month: 0, day: 0}, field: "", clinic: {location: "", name: ""},
                    educationalBackground: {school: "", years: 0}};
      this.doctorList = [];
    }

  ngOnInit(): void {
    if (this.data.SSN != '') {
      this.getDoctor(this.data.SSN);
    }
    this.getDoctorList();
  }

  getDoctor(SSN: string): void {
    this.doctorService.getDoctor(SSN).subscribe(
      (resp) => {this.doctor = (resp.data as Doctor); console.log(this.doctor)}
    );
  }

  getDoctorList(): void {
    this.doctorService.getDoctors().subscribe(
      (resp) => this.doctorList = (resp.data as string[])
    );
  }

  setDoctor(SSN: string): void {
    window.sessionStorage.setItem("userSSN", SSN);
    this.closeModal();
    window.location.reload();
  }

  closeModal(): void {
    this.dialogRef.close();
  }
}

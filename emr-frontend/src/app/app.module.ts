import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { JwPaginationModule } from 'jw-angular-pagination';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { MainPageComponent } from './main-page/main-page.component';
import { PatientsComponent } from './patients/patients.component';
import { UserComponent } from './user/user.component';
import { PatientModalComponent } from './patient-modal/patient-modal.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PatientViewModalComponent } from './patient-view-modal/patient-view-modal.component';
import { DoctorModalComponent } from './doctor-modal/doctor-modal.component';
import { MedicationModalComponent } from './medication-modal/medication-modal.component';
import { MedicationViewModalComponent } from './medication-view-modal/medication-view-modal.component';
import { MedicationsComponent } from './medications/medications.component';
import { MedicationSearchModalComponent } from './medication-search-modal/medication-search-modal.component'

@NgModule({
  declarations: [
    MainPageComponent,
    PatientsComponent,
    UserComponent,
    MedicationsComponent,
    MedicationModalComponent,
    PatientModalComponent,
    PatientViewModalComponent,
    DoctorModalComponent,
    MedicationViewModalComponent,
    MedicationSearchModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    JwPaginationModule,
    MatButtonModule,
    MatDialogModule,
    BrowserAnimationsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [MainPageComponent],
  entryComponents: [PatientModalComponent]
})
export class AppModule { }

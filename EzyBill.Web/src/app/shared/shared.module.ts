import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageFilterComponent } from './components/page-filter/page-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableComponent } from './components/table/table.component';
import { ModalComponent } from './components/modal/modal.component';
import { FormComponent } from './components/form/form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    PageFilterComponent,
    TableComponent,
    ModalComponent,
    FormComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  exports: [
    PageFilterComponent,
    TableComponent,
    ModalComponent,
    FormComponent
  ]
})
export class SharedModule { }

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { InputField } from '../../models/input-field.model';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent<TModel> {

  @Input() columnConfig: Array<InputField<TModel>> = [];
  @Input() records: Array<TModel> = [];
  @Output() newClick = new EventEmitter<void>();
  @Input() readonly = true;
  @Output() saveClick = new EventEmitter<TModel>();
  @Output() deleteClick = new EventEmitter<TModel>();
  editModel?: TModel;
  editIndex = -1;

  onEditClick(recordToEdit: TModel){
    this.editModel = {...recordToEdit};
  }

  onRecordColumnChange(key: keyof TModel, newValue: any){
    this.editModel = {...this.editModel, [key]: newValue} as TModel;
  }
}

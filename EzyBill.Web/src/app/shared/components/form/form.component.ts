import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { InputField } from '../../models/input-field.model';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
  exportAs:'appForm'
})
export class FormComponent<TModel> implements OnInit, OnDestroy{
  @Input() inputFields: Array<InputField<TModel>> =[];
  @Input() model?: TModel;
  @Input() colClass = 'col-4';
  @Output() appFormChange = new EventEmitter<TModel>();
  formGroup: FormGroup = new FormGroup({name: new FormControl()})
  private subscriptions = new Subscription();
  ngOnInit(){
    let fGroupConfig = this.inputFields.reduce((carry, next) => {
      return {...carry, [next.key]: new FormControl(this.model?.[next.key])}
    },<{[key:string]: FormControl}>{})
    this.formGroup = new FormGroup(fGroupConfig);
    this.subscriptions.add(this.formGroup.valueChanges.subscribe(val => {
      this.appFormChange.emit(val as TModel)
    }));
  }
  ngOnDestroy(){
    this.subscriptions.unsubscribe();
  }
   getControl(controlPath: string): FormControl{
    return this.formGroup.get(controlPath) as FormControl;
  }

  get formValue(){
    return this.formGroup.value as TModel;
  }
}

import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { InputField } from '../../models/input-field.model';;
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-page-filter',
  templateUrl: './page-filter.component.html',
  styleUrls: ['./page-filter.component.scss']
})
export class PageFilterComponent<TModel> implements OnInit{

  @Input() filterFields: Array<InputField<TModel>> = [];
  fb = inject(FormBuilder);
  @Output() filterClick = new EventEmitter<TModel>();
  constructor(){
  }

  ngOnInit(){
  }
 
}

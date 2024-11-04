import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { InputField } from '../../../../shared/models/input-field.model';
import { ICustomerDto } from '../../../../shared/interfaces/customer-dto';
import { CustomersConfig } from './customers.config';
import { IPagingPageQueryDto } from '../../../../shared/interfaces/paging-page-query.dto';
import { Store } from '@ngrx/store';
import { addNewCustomer, fetchCustomers, updateCustomer } from '../../state/customers/customers.actions';
import { Subscription } from 'rxjs';
import { Actions } from '@ngrx/effects';
import { selectCustomers } from '../../state/customers/customers.selectors';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit, OnDestroy {

  filterFields: Array<InputField<ICustomerDto>> = CustomersConfig.filterFields
  customerTableColumnConfig: Array<InputField<ICustomerDto>> = CustomersConfig.customerTableColumnConfig
  customerFormFields: Array<InputField<ICustomerDto>> = CustomersConfig.customerFormFields;
  private _store = inject(Store)
  customerRecords: Array<ICustomerDto> = [
    {email: 'maki.nkuna@dbe.gov.za', name: 'Maki', phone:'0720626744', id:''},
    {email: 'tsholo.nkuna@hartic.com', name: 'Tsholofelo', phone:'0762592125', id:''},
    {email:'morongwa.ramoncha@ccssa.com', name: 'Latifah', phone:'0676066263', id:''}
  ];
  
  showNewCustomerModal = false;
  newCustomerModel: ICustomerDto = {email:'',name:'',id:'',phone:''};
  pageQuery : IPagingPageQueryDto<ICustomerDto> = {filters: {email:'', name: '', phone: ''},pageIndex: 1, pageSize: 30, totalRecordCount:0};
  subscriptions = new Subscription();
  ngOnInit(){
    this.registerSubscriptions();
    this._store.dispatch(fetchCustomers({pageQuery: this.pageQuery}));
  }

  ngOnDestroy(){
    this.subscriptions.unsubscribe();
  }

  registerSubscriptions(){
   this.subscriptions.add(this._store.select(selectCustomers).subscribe(customers => {
     this.customerRecords = customers ?? [];
   }));
  }

  onCustomerSearchClick(filterValue: ICustomerDto){
    this.pageQuery.filters = {...filterValue};
    this._store.dispatch(fetchCustomers({pageQuery: this.pageQuery}));
  }
  onSaveNewCustomer(val: ICustomerDto | undefined){
    if(val){
      this._store.dispatch(addNewCustomer({customer: val}));
    }
  }

  onTableRecordSave(recordChanges: ICustomerDto){
    this._store.dispatch(updateCustomer({customer: recordChanges}));
  }
  onTableRecordDelete(recordToDelete: ICustomerDto){
    console.log(recordToDelete);
  }
}

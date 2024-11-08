import { Component, OnDestroy, OnInit, computed, inject, signal, effect } from '@angular/core';
import { InputField } from '../../../../shared/models/input-field.model';
import { ICustomerDto } from '../../../../shared/interfaces/customer-dto';
import { CustomersConfig } from './customers.config';
import { IPagingPageQueryDto } from '../../../../shared/interfaces/paging-page-query.dto';
import { Store } from '@ngrx/store';
import { addNewCustomer, addNewCustomerComplete, deleteCustomer, deleteCustomerComplete, fecthCustomersComplete, fetchCustomers, updateCustomer, updateCustomerComplete } from '../../state/customers/customers.actions';
import { Subscription } from 'rxjs';
import { Actions, ofType } from '@ngrx/effects';
import { customerPageBusyIndicatorSelector, selectCustomers } from '../../state/customers/customers.selectors';
import { CustomersViewBusyIndicator, CustomersViewState } from '../../state/customers/customers.reducer';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit, OnDestroy {

  filterFields: Array<InputField<ICustomerDto>> = CustomersConfig.filterFields
  customerTableColumnConfig: Array<InputField<ICustomerDto>> = CustomersConfig.customerTableColumnConfig
  customerFormFields: Array<InputField<ICustomerDto>> = CustomersConfig.customerFormFields;
  customerTableEditIndexSignal =  signal(-1);
  private _store: Store<CustomersViewState> = inject(Store)
  customerRecords: Array<ICustomerDto> = [];
  newCustomerModalAlertType: 'danger' | 'success' = 'danger';
  showNewCustomerModalSignal = signal(false);
  saveNewCustomerClickedSignal = signal(false);
  newCustomerModel: ICustomerDto = {email:'',name:'',id:'',phone:''};
  pageQuery : IPagingPageQueryDto<ICustomerDto> = {filters: {email:'', name: '', phone: ''},pageIndex: 1, pageSize: 30, totalRecordCount:0};
  subscriptions = new Subscription();
  customerViewBusySignal = signal<CustomersViewBusyIndicator>({
    customerFetchDone: false,
    customerUpdateDone: false,
    saveNewCustomerDone: false
  });
  customerTableRowSaveClickSignal = signal(false);
  newCustomerModalAlertVisibleSignal = signal(false);
  customerUpdateCompleteSignal = computed(() => this.customerTableEditIndexSignal() !== -1 && (this.customerViewBusySignal().customerUpdateDone ?? false));
  newCustomerAlertMessage = ''; 
  shouldCloseNewCustomerModalSignal = computed(() => !!this.customerViewBusySignal().saveNewCustomerDone && !this.showNewCustomerModalSignal() );
  newCustomerSaveDoneSignal = computed(() => this.customerViewBusySignal().saveNewCustomerDone??false);
  newCustomerSaveDoneWhileModalOpen = computed(() => this.newCustomerSaveDoneSignal() && !this.shouldCloseNewCustomerModalSignal());
  newCustomerSaveDoneSignalEf = effect(() => {
     if(this.newCustomerSaveDoneWhileModalOpen()){
       this.newCustomerAlertMessage = "Save Complete!"
       this.newCustomerModalAlertType = 'success'; 
     }
  });


  computedEditIndexInput = computed(() => this.customerUpdateCompleteSignal() ? -1 : this.customerTableEditIndexSignal())
  
  newCustomerAlertVisibleSignal = computed(() => this.newCustomerSaveDoneSignal() && this.saveNewCustomerClickedSignal())
  customerTableLoadingSignal = computed(() => {
     return (!this.customerViewBusySignal().customerUpdateDone && this.customerTableRowSaveClickSignal()) || !this.customerViewBusySignal().customerFetchDone; 
  });

  constructor(private $actions: Actions){

  }

  computeEditIndex(){
    if(this.customerUpdateCompleteSignal()){
      this.customerTableEditIndexSignal.set(-1);
    }
    return this.customerTableEditIndexSignal();
  }
  ngOnInit(){
    this.registerSubscriptions();
    this._store.dispatch(fetchCustomers({pageQuery: {...this.pageQuery}}));
  }


  ngOnDestroy(){
    this.subscriptions.unsubscribe();
    this.newCustomerSaveDoneSignalEf.destroy();
    
  }


  registerSubscriptions(){
   this.subscriptions.add(this._store.select(selectCustomers).subscribe(customers => {
     this.customerRecords = customers ?? [];
   }));
   this.subscriptions.add(this._store.select(customerPageBusyIndicatorSelector).subscribe(indicator=>{
        this.customerViewBusySignal.set({...indicator}); 
   }));

   this.subscriptions.add(this.$actions.pipe(
     ofType(updateCustomerComplete, addNewCustomerComplete, deleteCustomerComplete)).subscribe(action => {
       if(action.type == addNewCustomerComplete.type
         || action.type === updateCustomerComplete.type
         || action.type === deleteCustomerComplete.type){
          this._store.dispatch(fetchCustomers({pageQuery: {...this.pageQuery}}));
       }
      
     })
   )
  }
  onCustomerTableEditClick(){
    this.customerViewBusySignal.update(x => ({...x, customerUpdateDone : false}));
    this.customerTableRowSaveClickSignal.set(false);
  }
  onNewCustomerModalCloseClick(){
    this.customerViewBusySignal.update(x => ({...x, saveNewCustomerDone: true}));
  }
  onNewCustomerClick(){
    this.showNewCustomerModalSignal.set(true);
    this.saveNewCustomerClickedSignal.set(false);
  }

  onCustomerSearchClick(filterValue: ICustomerDto){

    this.pageQuery.filters = {...filterValue};
    this.pageQuery.filters!.email ??= ''  ;
    this.pageQuery.filters!.name ??= '';
    this.pageQuery.filters!.phone ??= '';
    this._store.dispatch(fetchCustomers({pageQuery: {...this.pageQuery}}));
  }
  onSaveNewCustomer(val: ICustomerDto | undefined){
    if(val){
      this._store.dispatch(addNewCustomer({customer: val}));
      this.saveNewCustomerClickedSignal.set(true);
      this.customerViewBusySignal.update(x => ({...x, saveNewCustomerDone: false}));
    }
  }

  onTableRecordSave(recordChanges: ICustomerDto){
    this._store.dispatch(updateCustomer({customer: recordChanges}));
    this.customerTableRowSaveClickSignal.set(true);
    
  }
  onTableRecordDelete(recordToDelete: ICustomerDto){
    this._store.dispatch(deleteCustomer({customerIdentifiers: [recordToDelete.id ?? '']}));
  }
}

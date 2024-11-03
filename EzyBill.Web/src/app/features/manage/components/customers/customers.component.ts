import { Component } from '@angular/core';
import { InputField } from '../../../../shared/models/input-field.model';
import { ICustomerDto } from '../../../../shared/interfaces/customer-dto';
import { CustomersConfig } from './customers.config';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent {

  filterFields: Array<InputField<ICustomerDto>> = CustomersConfig.filterFields
  customerTableColumnConfig: Array<InputField<ICustomerDto>> = CustomersConfig.customerTableColumnConfig
  customerFormFields: Array<InputField<ICustomerDto>> = CustomersConfig.customerFormFields;

  customerRecords: Array<ICustomerDto> = [
    {email: 'maki.nkuna@dbe.gov.za', name: 'Maki', phone:'0720626744', id:''},
    {email: 'tsholo.nkuna@hartic.com', name: 'Tsholofelo', phone:'0762592125', id:''},
    {email:'morongwa.ramoncha@ccssa.com', name: 'Latifah', phone:'0676066263', id:''}
  ];
  
  showNewCustomerModal = false;
  newCustomerModel: ICustomerDto = {email:'',name:'',id:'',phone:''};
  onCustomerSearchClick(filterValue: ICustomerDto){
    console.log(filterValue);
  }
  onSaveNewCustomer(val: ICustomerDto | undefined){
    console.log(val);
  }

  onTableRecordSave(recordChanges: ICustomerDto){
    console.log(recordChanges);
  }
  onTableRecordDelete(recordToDelete: ICustomerDto){
    console.log(recordToDelete);
  }
}

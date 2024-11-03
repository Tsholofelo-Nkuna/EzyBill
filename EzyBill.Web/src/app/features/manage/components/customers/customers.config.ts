import { ICustomerDto } from "../../../../shared/interfaces/customer-dto";
import { InputField } from "../../../../shared/models/input-field.model";

export class CustomersConfig{

  static filterFields: Array<InputField<ICustomerDto>> = [
    new InputField('Name', 'name', 'input'),
    new InputField('Email','email', 'input'),
    new InputField('Phone No.', 'phone', 'input')
  ];

  static customerTableColumnConfig: Array<InputField<ICustomerDto>> = [
    new InputField('Name', 'name', 'input'),
    new InputField('Email','email', 'input'),
    new InputField('Phone No.', 'phone', 'input')
  ];

  static customerFormFields: Array<InputField<ICustomerDto>> = [
    new InputField('Name', 'name', 'input'),
    new InputField('Email','email', 'input'),
    new InputField('Phone No.', 'phone', 'input')
  ];
}

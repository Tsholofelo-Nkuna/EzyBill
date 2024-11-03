export class InputField<TInModel>{

  readonly = false;
  constructor(public label: string, public key: keyof(TInModel), public controlType: 'input'|'select'|'checkbox'){}
}

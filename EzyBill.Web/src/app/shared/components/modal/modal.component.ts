import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent<TContext> implements OnInit{
  @Input() contentTemplate?: TemplateRef<{$implicit:TContext|undefined}>;
  @Input() templateContext?:TContext;
  @Input() title = '';
  @Input() show = false;
  @Output() showChange = new EventEmitter<boolean>();
  @Input() okText?: string;
  @Output() okClick = new EventEmitter<TContext|undefined>();
  @Output() closeClick = new EventEmitter();
  @Input() loading = false;
  @Input() alertType: 'success' | 'danger' | 'info' = 'info';
  @Input() alertMessage = '';
  @Input() alertVisible = false;
  @Output() alertVisibleChange = new EventEmitter<boolean>();
  @Output() alertCloseClick = new EventEmitter();
  ngOnInit(){

  }

  onOkClick(){
    
    this.okClick.emit(this.templateContext);
  }

  onAlertCloseClick(){
    this.alertVisible = false;
    this.alertVisibleChange.emit(this.alertVisible);
    this.alertCloseClick.emit();
  }

  onCloseModalClick(){
    this.show=false;
    this.showChange.emit(this.show);
    this.closeClick.emit();
  }
}

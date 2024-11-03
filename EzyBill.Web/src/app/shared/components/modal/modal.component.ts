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
  ngOnInit(){

  }

  onOk(){
     this.okClick.emit(this.templateContext);
  }
}
